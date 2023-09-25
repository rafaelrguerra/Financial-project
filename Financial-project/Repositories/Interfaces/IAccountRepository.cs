using Financial_project.Models;

namespace Financial_project.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        AccountModel GetAccountBalanceByAccountNumber(string accountNumber);
        List<AccountModel> GetAccountsByCustomerId(int costumerId);
        Task<AccountModel> CreateAccount(AccountModel account);
    }
}
