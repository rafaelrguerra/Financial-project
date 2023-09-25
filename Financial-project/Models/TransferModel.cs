using System.Text.Json.Serialization;

namespace Financial_project.Models
{
    public class TransferModel
    {
        public TransferModel()
        {
            CreatedAt = DateTime.Now;
        }
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int AccountIdFrom { get; set; }
        [JsonIgnore]
        public AccountModel? AccountFrom { get; set; }
        public int AccountIdTo { get; set; }
        [JsonIgnore]
        public AccountModel? AccountTo { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}