using Financial_project.Models;

namespace Financial_project.Repositories.Interfaces
{
    public interface ITransferRepository
    {
        TransferModel CreateTransfer(TransferModel model);
        List<TransferModel> GetTransfersByAccountNumber(string accountNumber);
    }
}