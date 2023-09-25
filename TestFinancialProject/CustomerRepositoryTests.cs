using Financial_project.Data;
using Financial_project.Models;
using Financial_project.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestFinancialProject
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        [TestMethod]
        public void GetAllCustomers_ExistingCustomers_ReturnsCustomers()
        {
            var data = new List<CustomerModel>
            {
                new CustomerModel { Id = 1, IdentityCard = "1234", Name = "Jack" },
                new CustomerModel { Id = 2, IdentityCard = "8482", Name = "Mary" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CustomerModel>>();
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var repository = new CustomerRepository(mockContext.Object);
            var result = repository.GetAllCustomers();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetAllCustomers_NoCustomersRegistered_ReturnsCustomers()
        {
            var data = new List<CustomerModel>().AsQueryable();

            var mockSet = new Mock<DbSet<CustomerModel>>();
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var repository = new CustomerRepository(mockContext.Object);
            var result = repository.GetAllCustomers();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetCustomerByIdentityCard_ExistingCustomer_ReturnsCustomer()
        {
            var data = new List<CustomerModel>
            {
                new CustomerModel { Id = 1, IdentityCard = "1234", Name = "Jack" },
                new CustomerModel { Id = 2, IdentityCard = "8482", Name = "Mary" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CustomerModel>>();
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var repository = new CustomerRepository(mockContext.Object);
            var result = repository.GetCustomerByIdentityCard("1234");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IdentityCard, "1234");
        }

        [TestMethod]
        public void GetCustomerByIdentityCard_NonExistingCustomer_ReturnsNull()
        {
            var data = new List<CustomerModel>
            {
                new CustomerModel { Id = 1, IdentityCard = "1234", Name = "Jack" },
                new CustomerModel { Id = 2, IdentityCard = "8482", Name = "Mary" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CustomerModel>>();
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var repository = new CustomerRepository(mockContext.Object);
            var result = repository.GetCustomerByIdentityCard("ABC");

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddCustomer_NonExistingCustomer_ReturnsNewCustomer()
        {
            var data = new List<CustomerModel>().AsQueryable();

            var mockSet = new Mock<DbSet<CustomerModel>>();
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var repository = new CustomerRepository(mockContext.Object);
            var result = await repository.AddCustomer(new CustomerModel() { IdentityCard = "1234", Name = "Joao" });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IdentityCard, "1234");
        }

        [TestMethod]
        public async Task AddCustomer_ExistingIdentityCard_ReturnsError()
        {
            var data = new List<CustomerModel>
            {
                new CustomerModel { Id = 1, IdentityCard = "1234", Name = "Jack" },
                new CustomerModel { Id = 2, IdentityCard = "8482", Name = "Mary" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CustomerModel>>();
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var repository = new CustomerRepository(mockContext.Object);

            try
            {
                var result = await repository.AddCustomer(new CustomerModel() { IdentityCard = "1234", Name = "Joao" });
                Assert.Fail("Expected an exception to be thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Customer with Identity Card 1234 already exists.", ex.Message);
            }
        }

        [TestMethod]
        public async Task AddCustomer_MissingData_ReturnsError()
        {
            var data = new List<CustomerModel>().AsQueryable();

            var mockSet = new Mock<DbSet<CustomerModel>>();
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CustomerModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FinancialProjectDBContext>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var repository = new CustomerRepository(mockContext.Object);
            try
            {
                var result = await repository.AddCustomer(new CustomerModel() { IdentityCard = "891024" });
                Assert.Fail("Expected an exception to be thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid parameters.", ex.Message);
            }
        }
    }
}