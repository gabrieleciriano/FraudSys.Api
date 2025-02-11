﻿using FraudSys.Domain.Entities.v1;

namespace FraudSys.Domain.Interfaces.v1.Repositories
{
    public interface IAccountLimitRepository
    {
        Task SaveAsync(AccountLimit entity);
        Task<AccountLimit> GetAccountLimitAsync(string cpf, string agencyNumber);
        Task UpdateAccountLimitAsync(string cpf, string agencyNumber, double newLimit);
        Task DeleteAccountLimitAsync(string cpf, string agencyNumber, string accountNumber);
        Task<bool> ProcessPixTransactionAsync(string cpf, string agencyNumber, string accountNumber, double transactionAmount);
    }
}