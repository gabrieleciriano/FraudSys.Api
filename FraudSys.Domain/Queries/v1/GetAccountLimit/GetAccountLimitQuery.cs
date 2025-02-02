using MediatR;

namespace FraudSys.Domain.Queries.v1.GetAccountLimit
{
    public sealed class GetAccountLimitQuery : IRequest<GetAccountLimitQueryResponse>
    {
        public string CPF { get; set; }
        public string AgencyNumber { get; set; }
    }
}