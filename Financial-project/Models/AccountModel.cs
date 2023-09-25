using System.Text.Json.Serialization;

namespace Financial_project.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
        [JsonIgnore]
        public CustomerModel? Customer { get; set; }
        [JsonIgnore]
        public List<TransferModel>? ReceivedTransfers { get; set; }
        [JsonIgnore]
        public List<TransferModel>? SentTransfers { get; set; }
    }
}
