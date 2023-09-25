using Financial_project.Data;
using Financial_project.Models;
using Financial_project.Repositories.Interfaces;

namespace Financial_project.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FinancialProjectDBContext _dbContext;
        public CustomerRepository(FinancialProjectDBContext financialProjectDBContext)
        {
            _dbContext = financialProjectDBContext;
        }
        public List<CustomerModel> GetAllCustomers()
        {
            return _dbContext.Customers.ToList();
        }

        public CustomerModel GetCustomerByIdentityCard(string identityCard)
        {
            return _dbContext.Customers.FirstOrDefault(x => x.IdentityCard == identityCard);
        }

        public async Task<CustomerModel> AddCustomer(CustomerModel customer)
        {
            var existingCustumer = GetCustomerByIdentityCard(customer.IdentityCard);
            if (existingCustumer != null)
            {
                throw new Exception($"Customer with Identity Card {customer.IdentityCard} already exists.");
            }

            if (string.IsNullOrEmpty(customer.Name) || string.IsNullOrEmpty(customer.IdentityCard))
                throw new Exception("Invalid parameters.");

            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }
    }
}