using FraudSys.Domain.Interfaces.v1.Repositories;
using MediatR;

namespace FraudSys.Domain.Queries.v1.GetAccountLimit
{
    public class GetAccountLimitQueryHandler : IRequestHandler<GetAccountLimitQuery, GetAccountLimitQueryResponse>
    {
       private readonly IAccountLimitRepository _accountLimitRepository;

        public GetAccountLimitQueryHandler(IAccountLimitRepository accountLimitRepository) 
        {
            _accountLimitRepository = accountLimitRepository;
        }

        public async Task<GetAccountLimitQueryResponse> Handle(GetAccountLimitQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var accountLimit = await _accountLimitRepository.GetAccountLimitAsync(request.CPF, request.AgencyNumber);

                if (accountLimit is null)
                {
                    throw new KeyNotFoundException("The limit was not found for this account.");
                }

                return new GetAccountLimitQueryResponse(accountLimit);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error Message: {ex}");
            }
        }
    }
}