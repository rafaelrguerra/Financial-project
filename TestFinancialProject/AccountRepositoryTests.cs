using Financial_project.Data;
using Financial_project.Models;
using Financial_project.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestFinancialProject
{
    [TestClass]
    public class AccountRepositoryTests
    {
        [TestMethod]
        public async Task GetAccountBalanceByAccountNumber_ExistingAccount_ReturnsAccount()
        {
            var data = new List<AccountModel>
            {
                new AccountModel { AccountNumber= "12345", Id = 1, Balance = 100, CustomerId = 1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<AccountModel>>();
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Accounts).Returns(mockSet.Object);

            var repository = new AccountRepository(mockContext.Object);
            var result = repository.GetAccountBalanceByAccountNumber("12345");

            Assert.IsNotNull(result);
            Assert.AreEqual("12345", result.AccountNumber);
        }

        [TestMethod]
        public async Task GetAccountBalanceByAccountNumber_NonExistingAccount_ReturnsError()
        {
            var data = new List<AccountModel>
            {
                new AccountModel { AccountNumber= "12345", Id = 1, Balance = 100, CustomerId = 1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<AccountModel>>();
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Accounts).Returns(mockSet.Object);

            var repository = new AccountRepository(mockContext.Object);
            var result = repository.GetAccountBalanceByAccountNumber("4321");

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAccountsByCustomerId_NonExistingAccount_ReturnsError()
        {
            var customers = new List<CustomerModel>
            {
                new CustomerModel { Id = 1, Name = "Test", IdentityCard = "001" }
            }.AsQueryable();

            var accounts = new List<AccountModel>
            {
                new AccountModel { AccountNumber= "12345", Id = 1, Balance = 100, CustomerId = 1 }
            }.AsQueryable();

            var mockSetAccounts = new Mock<DbSet<AccountModel>>();
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(accounts.Provider);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(accounts.Expression);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(accounts.ElementType);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(() => accounts.GetEnumerator());

            var mockSetCustomers = new Mock<DbSet<CustomerModel>>();
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.Provider).Returns(customers.Provider);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.Expression).Returns(customers.Expression);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.ElementType).Returns(customers.ElementType);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.GetEnumerator()).Returns(() => customers.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Accounts).Returns(mockSetAccounts.Object);
            mockContext.Setup(c => c.Customers).Returns(mockSetCustomers.Object);

            var repository = new AccountRepository(mockContext.Object);
            var result = repository.GetAccountsByCustomerId(2);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetAccountsByCustomerId_ExistingAccount_ReturnsAccount()
        {
            var customers = new List<CustomerModel>
            {
                new CustomerModel { Id = 1, Name = "Test", IdentityCard = "001" }
            }.AsQueryable();

            var accounts = new List<AccountModel>
            {
                new AccountModel { AccountNumber= "12345", Id = 1, Balance = 100, CustomerId = 1 }
            }.AsQueryable();

            var mockSetAccounts = new Mock<DbSet<AccountModel>>();
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(accounts.Provider);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(accounts.Expression);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(accounts.ElementType);
            mockSetAccounts.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(() => accounts.GetEnumerator());

            var mockSetCustomers = new Mock<DbSet<CustomerModel>>();
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.Provider).Returns(customers.Provider);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.Expression).Returns(customers.Expression);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.ElementType).Returns(customers.ElementType);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.GetEnumerator()).Returns(() => customers.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Accounts).Returns(mockSetAccounts.Object);
            mockContext.Setup(c => c.Customers).Returns(mockSetCustomers.Object);

            var repository = new AccountRepository(mockContext.Object);
            var result = repository.GetAccountsByCustomerId(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("12345", result.First().AccountNumber);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public async Task CreateAccount_AccountAlreadyExists_ThrowsException()
        {
            var data = new List<AccountModel>
            {
                new AccountModel { AccountNumber= "12345", Id = 1, Balance = 100, CustomerId = 1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<AccountModel>>();
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Accounts).Returns(mockSet.Object);

            var repository = new AccountRepository(mockContext.Object);
            try
            {
                var result = await repository.CreateAccount(new AccountModel() { AccountNumber = "12345" });
                Assert.Fail("Expected an exception to be thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("The account 12345 already exists.", ex.Message);
            }
        }

        [TestMethod]
        public async Task CreateAccount_AccountDoesNotExist_ReturnsAccount()
        {
            var customers = new List<CustomerModel>
            {
                new CustomerModel { Id = 2, IdentityCard = "001", Name = "Test" }
            }.AsQueryable();

            var accounts = new List<AccountModel>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<AccountModel>>();
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(accounts.Provider);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(accounts.Expression);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(accounts.ElementType);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(() => accounts.GetEnumerator());

            var mockSetCustomers = new Mock<DbSet<CustomerModel>>();
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.Provider).Returns(customers.Provider);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.Expression).Returns(customers.Expression);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.ElementType).Returns(customers.ElementType);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.GetEnumerator()).Returns(() => customers.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Accounts).Returns(mockSet.Object);
            mockContext.Setup(c => c.Customers).Returns(mockSetCustomers.Object);

            var repository = new AccountRepository(mockContext.Object);

            var result = await repository.CreateAccount(new AccountModel() { AccountNumber = "123", CustomerId = 2 });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateAccount_AccountWithInvalidCustomer_ReturnsError()
        {
            var data = new List<AccountModel>().AsQueryable();

            var customers = new List<CustomerModel>().AsQueryable();

            var mockSet = new Mock<DbSet<AccountModel>>();
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<AccountModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockSetCustomers = new Mock<DbSet<CustomerModel>>();
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.Provider).Returns(customers.Provider);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.Expression).Returns(customers.Expression);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.ElementType).Returns(customers.ElementType);
            mockSetCustomers.As<IQueryable<CustomerModel>>().Setup(m => m.GetEnumerator()).Returns(() => customers.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Accounts).Returns(mockSet.Object);
            mockContext.Setup(c => c.Customers).Returns(mockSetCustomers.Object);

            var repository = new AccountRepository(mockContext.Object);
            try
            {
                var result = await repository.CreateAccount(new AccountModel() { AccountNumber = "123" });
                Assert.Fail("Expected an exception to be thrown");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Customer not found.", ex.Message);
            }
        }
    }
}