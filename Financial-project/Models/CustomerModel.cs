using System.Text.Json.Serialization;

namespace Financial_project.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? IdentityCard { get; set; }
        [JsonIgnore]
        public List<AccountModel>? Accounts { get; set; }
    }
}
