using MediatR;

namespace FraudSys.Domain.Commands.v1.UpdateAccountLimit
{
    public sealed class UpdateAccountLimitCommand : IRequest
    {
        public string CPF { get; set; }
        public string AgencyNumber { get; set; }
        public string? AccountNumber { get; set; }
        public double NewLimit { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}