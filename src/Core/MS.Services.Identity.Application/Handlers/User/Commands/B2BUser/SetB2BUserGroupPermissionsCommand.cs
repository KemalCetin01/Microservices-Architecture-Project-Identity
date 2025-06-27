using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Application.Helpers;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.User.Commands.B2BUser;

public class SetB2BUserGroupPermissionsCommand : B2BUserRolePermissionRequestDTO, ICommand<DataResponse<bool>>
{
}
public sealed class SetB2BUserGroupPermissionsCommandHandler : BaseCommandHandler<SetB2BUserGroupPermissionsCommand, DataResponse<bool>>
{
    protected readonly IUserB2BService _userB2BService;
    //protected readonly IKeycloakGroupService _keycloakGroupService;
    private readonly KeycloakOptions _keycloakOptions;
    private readonly IMapper _mapper;

    public SetB2BUserGroupPermissionsCommandHandler(IUserB2BService userB2BService,
        IMapper mapper,
        IOptions<KeycloakOptions> options)
    {
        _userB2BService = userB2BService;
        _keycloakOptions = options.Value;
        _mapper = mapper;
    }


    public override async Task<DataResponse<bool>> Handle(SetB2BUserGroupPermissionsCommand request, CancellationToken cancellationToken)
    {
        //TODO: Servise taşınacak.
        var userDetail = await _userB2BService.GetByIdAsync(request.userId, cancellationToken);
        if (userDetail == null)
            throw new ResourceNotFoundException(B2BUserConstants.RecordNotFound);


        if (userDetail.UserGroupRoleId.ToString()!= request.roleId.ToString())
        {
            if (string.IsNullOrEmpty(request.roleId))
            {
                userDetail.UserGroupRoleId = null;
            }
            else
            {
                userDetail.UserGroupRoleId = new Guid(request.roleId);
            }
            var userB2BCommandDTO = _mapper.Map<UpdateB2BUserCommandDTO>(userDetail);
            await _userB2BService.UpdateAsync(userB2BCommandDTO, cancellationToken);
        }
      
      //  request.userId = userDetail.IdentityRefId;
      //  var result = await _keycloakGroupService.AssignB2BUserGroupsAndPermissionsAsync(request, _keycloakOptions.ecommerce_b2b_realm, cancellationToken);
        throw new NotImplementedException("set b2b user group permission function is not implemented yet..");

    }
}
