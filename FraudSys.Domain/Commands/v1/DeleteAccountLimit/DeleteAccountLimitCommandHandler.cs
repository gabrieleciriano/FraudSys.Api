using FraudSys.Domain.Interfaces.v1.Repositories;
using MediatR;

namespace FraudSys.Domain.Commands.v1.DeleteAccountLimit
{
    public class DeleteAccountLimitCommandHandler : IRequestHandler<DeleteAccountLimitCommand>
    {
        private readonly IAccountLimitRepository _accountLimitRepository;
        public DeleteAccountLimitCommandHandler(IAccountLimitRepository accountLimitRepository)
        {
            _accountLimitRepository = accountLimitRepository;
        }

        public async Task Handle(DeleteAccountLimitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _accountLimitRepository.DeleteAccountLimitAsync(request.CPF, request.AgencyNumber, request.AccountNumber);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Message: {ex}");
            }
        }
    }
}