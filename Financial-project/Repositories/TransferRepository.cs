using Financial_project.Data;
using Financial_project.Models;
using Financial_project.Repositories.Interfaces;

namespace Financial_project.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly FinancialProjectDBContext _dbContext;
        public TransferRepository(FinancialProjectDBContext financialProjectDBContext)
        {
            _dbContext = financialProjectDBContext;
        }
        public List<TransferModel> GetTransfersByAccountNumber(string accountNumber)
        {
            return _dbContext.Transfers.Where(x => x.AccountFrom.AccountNumber == accountNumber || x.AccountTo.AccountNumber == accountNumber).ToList();
        }

        public TransferModel CreateTransfer(TransferModel transfer)
        {
            if (transfer.AccountIdFrom == transfer.AccountIdTo)
                throw new Exception("Same account numbers.");

            var accountFrom = _dbContext.Accounts.FirstOrDefault(x => x.Id == transfer.AccountIdFrom);
            var accountTo = _dbContext.Accounts.FirstOrDefault(x => x.Id == transfer.AccountIdTo);

            if (accountFrom == null || accountTo == null)
                throw new Exception("Invalid account(s).");

            if (accountFrom.Balance >= transfer.Amount)
            {
                accountFrom.Balance -= transfer.Amount;
                accountTo.Balance += transfer.Amount;

                _dbContext.Transfers.Add(transfer);
                _dbContext.SaveChanges();

                return new TransferModel() { Id = transfer.Id, Amount = transfer.Amount, AccountIdFrom = transfer.AccountIdFrom, AccountIdTo = transfer.AccountIdTo };
            }

            throw new Exception("Insufficient balance.");
        }
    }
}
