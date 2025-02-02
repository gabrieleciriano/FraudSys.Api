using FraudSys.Domain.Entities.v1;
using FraudSys.Domain.Interfaces.v1.Repositories;
using MediatR;

namespace FraudSys.Domain.Commands.v1.CreateAccountLimit
{
    public class CreateAccountLimitCommandHandler : IRequestHandler<CreateAccountLimitCommand, bool>
    {
        private readonly IAccountLimitRepository _accountLimitRepository;

        public CreateAccountLimitCommandHandler(IAccountLimitRepository accountLimitRepository)
        {
            _accountLimitRepository = accountLimitRepository;
        }

        public async Task<bool> Handle(CreateAccountLimitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.CPF) || string.IsNullOrEmpty(request.AgencyNumber) ||
                    string.IsNullOrEmpty(request.AccountNumber) || request.PixLimit <= 0)

                    throw new ArgumentException("CPF, AgencyNumber,AccountNumber and PixLimit fields are mandatory. Please, provide them.");

                var existingAccountLimit = await _accountLimitRepository.GetAccountLimitAsync(request.CPF, request.AgencyNumber);
                if (existingAccountLimit != null)
                {
                    throw new ArgumentException("The limit for this account has already been created.");
                   
                }
                else
                {
                    var accountLimit = new AccountLimit(request.CPF, request.AgencyNumber, request.AccountNumber, request.PixLimit);
                    
                    await _accountLimitRepository.SaveAsync(accountLimit);

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Message: {ex}");
               
            }
        }
    }
}