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
            //implementar try catch
            //se der tempo, implementar logs
            //verificar questão de todos os campos serem obrigatórios.
            //talvez implementar que não se pode registrar um limite caso já exista, somente alterar.
            var accountLimit = new AccountLimit(request.CPF, request.AgencyNumber, request.AccountNumber, request.PixLimit);

            await _accountLimitRepository.SaveAsync(accountLimit);

            return true;
        }
    }
}