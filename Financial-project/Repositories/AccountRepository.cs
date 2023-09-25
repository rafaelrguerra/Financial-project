using Financial_project.Data;
using Financial_project.Models;
using Financial_project.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Financial_project.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FinancialProjectDBContext _dbContext;
        public AccountRepository(FinancialProjectDBContext financialProjectDBContext)
        {
            _dbContext = financialProjectDBContext;
        }

        public AccountModel GetAccountBalanceByAccountNumber(string accountNumber)
        {
            return _dbContext.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
        }

        public List<AccountModel> GetAccountsByCustomerId(int costumerId)
        {
            return _dbContext.Accounts.Where(x => x.CustomerId == costumerId).ToList();
        }

        public async Task<AccountModel> CreateAccount(AccountModel account)
        {
            var existingAccount = GetAccountBalanceByAccountNumber(account.AccountNumber);

            if (existingAccount != null)
            {
                throw new Exception($"The account {account.AccountNumber} already exists.");
            }

            var existingCustomer = _dbContext.Customers.FirstOrDefault(x => x.Id == account.CustomerId);

            if (existingCustomer == null)
                throw new Exception($"Customer not found.");

            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return account;
        }
    }
}
