using Financial_project.Data;
using Financial_project.Models;
using Financial_project.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestFinancialProject
{
    [TestClass]
    public class TransferRepositoryTests
    {
        [TestMethod]
        public async Task GetTransfersByAccountNumber_ExistingAccount_ReturnsAccount()
        {
            var data2 = new List<AccountModel>
            {
                new AccountModel {Id = 1, AccountNumber = "0001", CustomerId = 1},
                new AccountModel {Id = 2, AccountNumber = "0002", CustomerId = 2},
                new AccountModel {Id = 3, AccountNumber = "0003", CustomerId = 3},
                new AccountModel {Id = 4, AccountNumber = "0004", CustomerId = 4},
            }.AsQueryable();

            var data = new List<TransferModel>
            {
                new TransferModel { AccountIdFrom =1, AccountIdTo = 2, Id = 1, Amount = 300, AccountFrom = data2.FirstOrDefault(x => x.Id == 1), AccountTo = data2.FirstOrDefault(x => x.Id == 2) },
                new TransferModel { AccountIdFrom =4, AccountIdTo = 1, Id = 2, Amount = 200, AccountFrom = data2.FirstOrDefault(x => x.Id == 4), AccountTo = data2.FirstOrDefault(x => x.Id == 1) },
                new TransferModel { AccountIdFrom =2, AccountIdTo = 3, Id = 3, Amount = 150, AccountFrom = data2.FirstOrDefault(x => x.Id == 2), AccountTo = data2.FirstOrDefault(x => x.Id == 3)  }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<TransferModel>>();
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockSet2 = new Mock<DbSet<AccountModel>>();
            mockSet2.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(data2.Provider);
            mockSet2.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(data2.Expression);
            mockSet2.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(data2.ElementType);
            mockSet2.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(() => data2.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Transfers).Returns(mockSet.Object);
            mockContext.Setup(c => c.Accounts).Returns(mockSet2.Object);

            var repository = new TransferRepository(mockContext.Object);
            var result = repository.GetTransfersByAccountNumber("0001");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetTransfersByAccountNumber_NonExistingAccount_ReturnsEmpty()
        {
            var data2 = new List<AccountModel>().AsQueryable();

            var data = new List<TransferModel>().AsQueryable();

            var mockSet = new Mock<DbSet<TransferModel>>();
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockSet2 = new Mock<DbSet<AccountModel>>();
            mockSet2.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(data2.Provider);
            mockSet2.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(data2.Expression);
            mockSet2.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(data2.ElementType);
            mockSet2.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(() => data2.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Transfers).Returns(mockSet.Object);
            mockContext.Setup(c => c.Accounts).Returns(mockSet2.Object);

            var repository = new TransferRepository(mockContext.Object);
            var result = repository.GetTransfersByAccountNumber("0001");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task CreateTransfer_SameAccountTransfer_ReturnsError()
        {
            var data = new List<TransferModel>().AsQueryable();

            var mockSet = new Mock<DbSet<TransferModel>>();
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Transfers).Returns(mockSet.Object);

            var repository = new TransferRepository(mockContext.Object);

            var transfer = new TransferModel()
            {
                Amount = 300,
                AccountIdFrom = 1,
                AccountIdTo = 1
            };

            try
            {
                var result = repository.CreateTransfer(transfer);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Same account numbers.", ex.Message);
            }

        }

        [TestMethod]
        public async Task CreateTransfer_NotEnoughBalanceTransfer_ReturnsError()
        {
            var accounts = new List<AccountModel>
            {
                new AccountModel{ Id = 1, Balance = 100, CustomerId = 1 },
                new AccountModel {Id = 2, Balance = 400, CustomerId = 2}
            }.AsQueryable();

            var transfers = new List<TransferModel>().AsQueryable();

            var mockSet = new Mock<DbSet<TransferModel>>();
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Provider).Returns(transfers.Provider);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Expression).Returns(transfers.Expression);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.ElementType).Returns(transfers.ElementType);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.GetEnumerator()).Returns(() => transfers.GetEnumerator());

            var mockSetAccounts = new Mock<DbSet<AccountModel>>();
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(accounts.Provider);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(accounts.Expression);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(accounts.ElementType);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(accounts.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Transfers).Returns(mockSet.Object);
            mockContext.Setup(c => c.Accounts).Returns(mockSetAccounts.Object);

            var repository = new TransferRepository(mockContext.Object);

            var transfer = new TransferModel()
            {
                Amount = 300,
                AccountIdFrom = 1,
                AccountIdTo = 2,
            };

            try
            {
                var result = repository.CreateTransfer(transfer);
                Assert.Fail("Expected an exception to be thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Insufficient balance.", ex.Message);
            }
        }

        [TestMethod]
        public async Task CreateTransfer_InvalidAccounts_ReturnsError()
        {
            var accounts = new List<AccountModel>
            {
                // No accounts registered in the database
            }.AsQueryable();

            var transfers = new List<TransferModel>().AsQueryable();

            var mockSet = new Mock<DbSet<TransferModel>>();
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Provider).Returns(transfers.Provider);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Expression).Returns(transfers.Expression);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.ElementType).Returns(transfers.ElementType);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.GetEnumerator()).Returns(() => transfers.GetEnumerator());

            var mockSetAccounts = new Mock<DbSet<AccountModel>>();
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(accounts.Provider);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(accounts.Expression);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(accounts.ElementType);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(accounts.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Transfers).Returns(mockSet.Object);
            mockContext.Setup(c => c.Accounts).Returns(mockSetAccounts.Object);

            var repository = new TransferRepository(mockContext.Object);

            var transfer = new TransferModel()
            {
                Amount = 300,
                AccountIdFrom = 1,
                AccountIdTo = 2,
            };

            try
            {
                var result = repository.CreateTransfer(transfer);
                Assert.Fail("Expected an exception to be thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid account(s).", ex.Message);
            }
        }

        [TestMethod]
        public async Task CreateTransfer_EnoughBalanceTransfer_ReturnsTransfer()
        {
            var accounts = new List<AccountModel>
            {
                new AccountModel{ Id = 1, Balance = 1000, CustomerId = 1 },
                new AccountModel {Id = 2, Balance = 400, CustomerId = 2}
            }.AsQueryable();

            var transfers = new List<TransferModel>().AsQueryable();

            var mockSet = new Mock<DbSet<TransferModel>>();
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Provider).Returns(transfers.Provider);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.Expression).Returns(transfers.Expression);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.ElementType).Returns(transfers.ElementType);
            mockSet.As<IQueryable<TransferModel>>().Setup(m => m.GetEnumerator()).Returns(() => transfers.GetEnumerator());

            var mockSetAccounts = new Mock<DbSet<AccountModel>>();
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(accounts.Provider);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(accounts.Expression);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(accounts.ElementType);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(accounts.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Transfers).Returns(mockSet.Object);
            mockContext.Setup(c => c.Accounts).Returns(mockSetAccounts.Object);

            var repository = new TransferRepository(mockContext.Object);

            var transfer = new TransferModel()
            {
                Amount = 300,
                AccountIdFrom = 1,
                AccountIdTo = 2,
            };

            var result = repository.CreateTransfer(transfer);
            Assert.IsNotNull(result);
        }
    }
}
