using Financial_project.Models;
using Financial_project.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Financial_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("getBalance/{accountNumber}")]
        public ActionResult<CustomerModel> GetAccountBalanceByAccountNumber(string accountNumber)
        {
            var account = _accountRepository.GetAccountBalanceByAccountNumber(accountNumber);
            if (account != null)
                return Ok(account);
            return NotFound($"Account number {accountNumber} not found.");
        }

        [HttpGet("getAccounts/{customerId}")]
        public ActionResult<CustomerModel> GetAccountsByCustomerId(int customerId)
        {
            var account = _accountRepository.GetAccountsByCustomerId(customerId);
            if (account != null && account.Count > 0)
                return Ok(account);
            return NotFound($"No accounts found for Customer ID {customerId}.");
        }

        [HttpPost("CreateAccount")]
        public async Task<ActionResult<CustomerModel>> CreateAccount([FromBody] AccountModel account, decimal initialDeposit)
        {
            try
            {
                account.Balance = initialDeposit;
                var createdAccount = await _accountRepository.CreateAccount(account);
                return Ok(createdAccount);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}