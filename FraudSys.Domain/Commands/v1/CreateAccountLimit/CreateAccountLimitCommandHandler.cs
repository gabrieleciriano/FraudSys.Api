using FraudSys.Domain.Entities.v1;
using FraudSys.Domain.Interfaces.v1.Repositories;
using MediatR;

namespace FraudSys.Domain.Commands.v1.CreateAccountLimit
{
    public class CreateAccountLimitCommandHandler : IRequestHandler<CreateAccountLimitCommand, bool>
    {
        private readonly IAccountLimitRepository _accountLimitRepository;
        public async Task<bool> Handle(CreateAccountLimitCommand request, CancellationToken cancellationToken)
        {
            var accountLimit = new AccountLimit(request.CPF, request.AgencyNumber, request.AccountNumber, request.PixLimit);

            await _accountLimitRepository.SaveAsync(accountLimit);

            return true;
        }
    }
}