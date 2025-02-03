using MediatR;

namespace FraudSys.Domain.Commands.v1.ProcessPixTransaction
{
    public sealed class ProcessPixTransactionCommand : IRequest<bool>
    {
        public string CPF { get; set; }
        public string AgencyNumber { get; set; }
        public string AccountNumber { get; set; }
        public double TransactionAmount { get; set; }
    }
}