
namespace AtmSystem.Data.Entities
{
    public class InternalTransfer
    {
        public decimal TransferAmount { get; set; }
        public int RecipientAccountNumber { get; set; }
        public string RecipientAccountName { get; set; }
    }
}
