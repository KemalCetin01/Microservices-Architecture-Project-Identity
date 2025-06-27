using Amazon.Runtime;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Enums;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services.File;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Extensions;
using MS.Services.Identity.Application.Handlers.Files.DTOs.Request;
using MS.Services.Identity.Application.Helpers.Options;
using MS.Services.Identity.Domain.Entities;

namespace MS.Services.Identity.Infrastructure.Services.File;

public class FileService : IFileService
{
    private readonly CdnOptions _cdnOptions;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IDocumentRepository _documentRepository;


    public FileService(IOptions<CdnOptions> options, IIdentityUnitOfWork identityUnitOfWork,
        IDocumentRepository documentRepository)
    {
        _cdnOptions = options.Value;
        _identityUnitOfWork = identityUnitOfWork;
        _documentRepository = documentRepository;
    }
    
    public async Task<string> UploadFileToCdnAsync(FileUploadRequestDto request,
        CancellationToken cancellationToken)
    {
        var fileName = request.File.FileName;
        if (!Path.GetExtension(request.File.FileName).IsAllowedExtension())
            throw new ApiException("Bu dosya türüne izin verilmiyor");
        
        await using var memoryStream = new MemoryStream();
        await request.File.CopyToAsync(memoryStream, cancellationToken);
        
        var config = new AmazonS3Config
        {
            ServiceURL = _cdnOptions.ServiceUrl,
            UseHttp = true
        };

        var stream = new MemoryStream(memoryStream.ToArray());

        using var client = new AmazonS3Client(_cdnOptions.ApiKey, _cdnOptions.SecretKey, config);
        var uniqueCode = Guid.NewGuid().ToString();

        var uniqueFileName = string.Concat(Path.GetFileNameWithoutExtension(fileName), "-", uniqueCode.AsSpan(0, 5));
        var fileExtension = Path.GetExtension(fileName);

        var uploadName = uniqueFileName + fileExtension;
        var accessType = GetAwsCannedAclBy(request.AccessControlType);
        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            Key = uploadName,
            BucketName = _cdnOptions.BucketName,
            CannedACL = accessType
        };

        var fileTransferUtility = new TransferUtility(client);
        await fileTransferUtility.UploadAsync(uploadRequest, cancellationToken);

        var cdnUrl = _cdnOptions.Path + "/" + uniqueFileName;

        var document = new Document
        {
            Name = fileName,
            UploadName = uploadName,
            Path = cdnUrl,
            ContentType = request.File.ContentType,
            IsPrivate = accessType != S3CannedACL.PublicRead || accessType != S3CannedACL.PublicReadWrite
        };

        if (request.Relation != null)
        {
            document.DocumentRelations = new List<DocumentRelation>();
            var documentRelation = new DocumentRelation
            {
                Entity = request.Relation.Entity,
                EntityId = request.Relation.EntityId,
                EntityField = request.Relation.EntityField,
                Order = request.Relation.Order
            };
            document.DocumentRelations.Add(documentRelation);
        }

        await _documentRepository.AddAsync(document, cancellationToken);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
        
        return cdnUrl;
    }

    public async Task<string> GetFilePath(string path, DateTime expires)
    {
        await using var memoryStream = new MemoryStream();

        var config = new AmazonS3Config
        {
            ServiceURL = _cdnOptions.ServiceUrl,
            SignatureMethod = SigningAlgorithm.HmacSHA256
        };

        var credentials = new BasicAWSCredentials(_cdnOptions.ApiKey, _cdnOptions.SecretKey);

        using var client = new AmazonS3Client(credentials, config);
        AWSConfigsS3.UseSignatureVersion4 = true;

        var request = new GetPreSignedUrlRequest
        {
            BucketName = _cdnOptions.BucketName,
            Key = path,
            Expires = expires,
            Verb = HttpVerb.GET
        };
        var url = client.GetPreSignedURL(request);
        return url;
    }

    #region Private Methods

    private static S3CannedACL GetAwsCannedAclBy(AWSAccessControlType accessControlType)
    {
        return accessControlType switch
        {
            AWSAccessControlType.NoACL => S3CannedACL.NoACL,
            AWSAccessControlType.Private => S3CannedACL.Private,
            AWSAccessControlType.PublicRead => S3CannedACL.PublicRead,
            AWSAccessControlType.PublicReadWrite => S3CannedACL.PublicReadWrite,
            AWSAccessControlType.AuthenticatedRead => S3CannedACL.AuthenticatedRead,
            AWSAccessControlType.AWSExecRead => S3CannedACL.AWSExecRead,
            AWSAccessControlType.BucketOwnerRead => S3CannedACL.BucketOwnerRead,
            AWSAccessControlType.BucketOwnerFullControl => S3CannedACL.BucketOwnerFullControl,
            AWSAccessControlType.LogDeliveryWrite => S3CannedACL.LogDeliveryWrite,
            _ => throw new ArgumentOutOfRangeException(nameof(accessControlType), accessControlType, null)
        };
    }
    #endregion
}