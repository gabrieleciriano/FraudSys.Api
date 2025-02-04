using MediatR;

namespace FraudSys.Domain.Commands.v1.CreateAccountLimit
{
    public sealed class CreateAccountLimitCommand : IRequest<bool>
    {
        public Guid CorrelationId => Guid.NewGuid();

        public string CPF { get; set; }

        public string AgencyNumber { get; set; }

        public string AccountNumber { get; set; }

        public double PixLimit { get; set; }

        public CreateAccountLimitCommand()
        {

        }

        public CreateAccountLimitCommand(string cpf, string agencyNumber, string accountNumber, double pixLimit)
        {
            CPF = cpf;
            AgencyNumber = agencyNumber;
            AccountNumber = accountNumber;
            PixLimit = pixLimit;
        }
    }
}