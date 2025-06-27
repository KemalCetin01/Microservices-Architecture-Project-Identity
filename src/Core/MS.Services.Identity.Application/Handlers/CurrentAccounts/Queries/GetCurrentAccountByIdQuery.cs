using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.DTOs;

namespace MS.Services.Identity.Application.Handlers.CurrentAccounts.Queries
{
    public class GetCurrentAccountByIdQuery : IQuery<CurrentAccountDTO>
    {
        public Guid Id { get; set; }
    }

    public sealed class GetCurrentAccountByIdHandler : BaseQueryHandler<GetCurrentAccountByIdQuery, CurrentAccountDTO>
    {
        protected readonly ICurrentAccountService _currentAccountService;

        public GetCurrentAccountByIdHandler(ICurrentAccountService currentAccountService)
        {
            _currentAccountService = currentAccountService;
        }
        public override async Task<CurrentAccountDTO> Handle(GetCurrentAccountByIdQuery request, CancellationToken cancellationToken  )
        {
            return await _currentAccountService.GetById(request.Id, cancellationToken);
        }
    }
}
