using Financial_project.Models;

namespace Financial_project.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        List<CustomerModel> GetAllCustomers();
        CustomerModel GetCustomerByIdentityCard(string identityCard);
        Task<CustomerModel> AddCustomer(CustomerModel customer);
    }
}
