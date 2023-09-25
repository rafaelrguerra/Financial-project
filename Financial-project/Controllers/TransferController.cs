using Financial_project.Models;
using Financial_project.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Financial_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IAccountRepository _accountRepository;
        public TransferController(ITransferRepository transferRepository, IAccountRepository accountRepository)
        {
            _transferRepository = transferRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet("getTransfers/{accountNumber}")]
        public ActionResult<List<TransferModel>> GetTransfersByAccountNumber(string accountNumber)
        {
            var transfers = _transferRepository.GetTransfersByAccountNumber(accountNumber);
            return Ok(transfers);
        }

        [HttpPost("createTransfer")]
        public ActionResult<TransferModel> CreateTransfer([FromBody] TransferModel transfer)
        {
            try
            {
                var transfers = _transferRepository.CreateTransfer(transfer);
                return Ok(transfers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
