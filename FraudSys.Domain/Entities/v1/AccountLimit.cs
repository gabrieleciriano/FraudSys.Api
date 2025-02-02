using Amazon.DynamoDBv2.DataModel;

namespace FraudSys.Domain.Entities.v1
{
    [DynamoDBTable("AccountLimit")]
    public sealed class AccountLimit
    {
        [DynamoDBHashKey] // Chave primária (Partition Key)
        public string CPF { get; set; }

        [DynamoDBRangeKey] // Sort Key
        public string AgencyNumber { get; set; }

        [DynamoDBProperty]
        public string AccountNumber { get; set; }

        [DynamoDBProperty]
        public double PixLimit { get; set; }

        [DynamoDBProperty]
        public DateTime CreatedAt { get; set; }

        [DynamoDBProperty]
        public DateTime UpdatedAt { get; set; }

        public AccountLimit() { }

        public AccountLimit(string cpf, string agencyNumber, string accountNumber, double pixLimit)
        {
            CPF = cpf;
            AgencyNumber = agencyNumber;
            AccountNumber = accountNumber;
            PixLimit = pixLimit;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public AccountLimit(string cpf, string agencyNumber)
        {
            CPF = cpf;
            AgencyNumber = agencyNumber;
        }
    }
}