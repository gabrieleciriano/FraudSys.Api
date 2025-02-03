using FraudSys.Domain.Interfaces.v1.Repositories;
using MediatR;

namespace FraudSys.Domain.Commands.v1.ProcessPixTransaction
{
    public class ProcessPixTransactionCommandHandler : IRequestHandler<ProcessPixTransactionCommand, bool>
    {
        private readonly IAccountLimitRepository _accountLimitRepository;

        public ProcessPixTransactionCommandHandler(IAccountLimitRepository accountLimitRepository)
        {
            _accountLimitRepository = accountLimitRepository;
        }

        public async Task<bool> Handle(ProcessPixTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _accountLimitRepository.ProcessPixTransactionAsync(
                    request.CPF,
                    request.AgencyNumber,
                    request.AccountNumber,
                    request.TransactionAmount
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Message: {ex}");
            }
        }
    }
}