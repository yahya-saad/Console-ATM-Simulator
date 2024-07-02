namespace AtmSystem.Data.Entities
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public short CardPin { get; set; }
        public string FullName { get; set; }
        public decimal Balance { get; set; }
        public int TotalLogin { get; set; } = 0;
        public bool IsLocked { get; set; } = false;

    }
}
