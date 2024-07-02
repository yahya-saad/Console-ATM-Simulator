namespace AtmSystem.Data.Entities
{
    public class Transaction
    {
        private static int _lastTransactionId = 0;

        public int TransactionId { get; private set; }
        public int UserAccountId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; private set; } = DateTime.Now;

        public Transaction()
        {
            TransactionId = ++_lastTransactionId;
        }
    }
}
