using FraudSys.Domain.Interfaces.v1.Repositories;
using MediatR;

namespace FraudSys.Domain.Commands.v1.UpdateAccountLimit
{
    public class UpdateAccountLimitCommandHandler : IRequestHandler<UpdateAccountLimitCommand>
    {
        private readonly IAccountLimitRepository _accountLimitRepository;

        public UpdateAccountLimitCommandHandler(IAccountLimitRepository accountLimitRepository)
        {
            _accountLimitRepository = accountLimitRepository;
        }
        public async Task Handle(UpdateAccountLimitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _accountLimitRepository.UpdateAccountLimitAsync(request.CPF, request.AgencyNumber, request.NewLimit);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Message: {ex}");
            }
        }
    }
}