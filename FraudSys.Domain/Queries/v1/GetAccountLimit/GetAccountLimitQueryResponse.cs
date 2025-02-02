using FraudSys.Domain.Entities.v1;

namespace FraudSys.Domain.Queries.v1.GetAccountLimit
{
    public class GetAccountLimitQueryResponse
    {
        public Guid CorrelationId => Guid.NewGuid();

        public string CPF { get; set; }

        public string AgencyNumber { get; set; }

        public string AccountNumber { get; set; }

        public double PixLimit { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        public GetAccountLimitQueryResponse(AccountLimit accountLimit)
        {
            CPF = accountLimit.CPF;
            AgencyNumber = accountLimit.AgencyNumber;
            AccountNumber = accountLimit.AccountNumber;
            PixLimit = accountLimit.PixLimit;
            CreatedAt = accountLimit.CreatedAt;
            UpdatedAt = accountLimit.UpdatedAt;
        }
    }
}