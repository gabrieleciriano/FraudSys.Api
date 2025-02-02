using Amazon.DynamoDBv2.DataModel;
using FraudSys.Domain.Entities.v1;
using FraudSys.Domain.Interfaces.v1.Repositories;

namespace FraudSys.Infrastructure.Data.Dynamo.v1.Repositories
{
    public class AccountLimitRepository : IAccountLimitRepository
    {
        private readonly IDynamoDBContext _context;

        public AccountLimitRepository(IDynamoDBContext context)
        {
            _context = context;
        }
        public async Task SaveAsync(AccountLimit entity)
        {
            await _context.SaveAsync(entity);
        }
    }
}