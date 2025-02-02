using FraudSys.Domain.Entities.v1;

namespace FraudSys.Domain.Interfaces.v1.Repositories
{
    public interface IAccountLimitRepository
    {
        Task SaveAsync(AccountLimit entity);
    }
}