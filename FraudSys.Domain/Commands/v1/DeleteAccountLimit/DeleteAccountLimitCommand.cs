using MediatR;

namespace FraudSys.Domain.Commands.v1.DeleteAccountLimit
{
    public sealed class DeleteAccountLimitCommand : IRequest
    {
        public string CPF { get; set; }
        public string AgencyNumber { get; set; }
        public string AccountNumber { get; set; }
    }
}