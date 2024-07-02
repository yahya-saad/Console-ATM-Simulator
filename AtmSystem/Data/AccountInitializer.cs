namespace AtmSystem.Data
{
    public class AccountInitializer
    {
        private static AccountInitializer instance;
        private List<Account> accounts;
        private List<Transaction> transactions;

        private AccountInitializer()
        {
            accounts = InitializeAccounts();
            transactions = new List<Transaction>();
        }

        public static AccountInitializer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountInitializer();
                }
                return instance;
            }
        }

        public List<Account> Accounts => accounts;
        public List<Transaction> Transactions => transactions;

        private static List<Account> InitializeAccounts()
        {
            return new List<Account>
            {
                new Account
                {
                    AccountNumber = 1001,
                    CardNumber = "12341234",
                    CardPin = 1234,
                    FullName = "Yahya Saad",
                    Balance = 45000.00m,
                },
                new Account
                {
                    AccountNumber = 1002,
                    CardNumber = "23452345",
                    CardPin = 2345,
                    FullName = "Osama Zoblex",
                    Balance = 40000.00m,
                },
                new Account
                {
                    AccountNumber = 1003,
                    CardNumber = "34563456",
                    CardPin = 3456,
                    FullName = "Ahmed Nagy",
                    Balance = 50000.00m,
                },
                new Account
                {
                    AccountNumber = 1004,
                    CardNumber = "45674567",
                    CardPin = 4567,
                    FullName = "Test User",
                    Balance = 20000.00m,
                }
            };
        }
    }
}
