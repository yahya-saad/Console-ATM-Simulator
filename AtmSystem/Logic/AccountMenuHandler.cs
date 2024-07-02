namespace AtmSystem.Logic
{
    public class AccountMenuHandler
    {
        private readonly Account _account;
        private readonly List<Account> _accounts = AccountInitializer.Instance.Accounts;
        private List<Transaction> _transactions = AccountInitializer.Instance.Transactions;
        private readonly Screen _screen = new Screen();
        public AccountMenuHandler(Account account)
        {
            _account = account;
        }
        public void HandleCommand(int choice)
        {
            switch (choice)
            {

                case ((int)AccountMenuOptions.CheckBalance):
                    CheckBalance();
                    break;
                case ((int)AccountMenuOptions.PlaceDeposite):
                    PlaceDeposite();
                    break;

                case ((int)AccountMenuOptions.MakeWithdrawal):
                    MakeWithdrawal();
                    break;

                case ((int)AccountMenuOptions.Transfer):
                    var transferForm = _screen.InternalTransferForm();
                    ProcessInternalTransfer(transferForm);
                    break;

                case ((int)AccountMenuOptions.ViewTranaction):
                    ViewTransactions();
                    break;

                case ((int)AccountMenuOptions.Logout):
                    Console.WriteLine("Logout");
                    break;

                default:
                    Utility.PrintMessage("Invalid Option..please try again", true);
                    break;

            }
        }

        private void CheckBalance()
        {
            Utility.PrintMessage($"Your Account Balance is {_account.Balance:C2}");
        }

        private void PlaceDeposite()
        {
            Console.WriteLine("Max amount to deposite is 50,000");
            Console.Write(">> ");
            bool isValidAmount = int.TryParse(Console.ReadLine(), out var amount);

            if (!isValidAmount || amount <= 0 || amount > 50000 || amount % 100 != 0)
            {
                Utility.PrintMessage("\nInvalid Amount. The amount must be between 100 and 50,000 and in multiples of 100.", true);
                return;
            }

            InsertTransaction(_account.AccountNumber, TransactionType.Deposite, amount);
            Console.WriteLine("Checking and Counting Money");
            Utility.PrintDotsAnimation();

            _account.Balance += amount;
            Console.WriteLine("Money Added to your account");
            CheckBalance();

        }

        private void MakeWithdrawal()
        {
            Console.WriteLine("Enter withdrawal amount (multiples of 100): ");
            Console.Write(">> ");

            bool isValidAmount = int.TryParse(Console.ReadLine(), out var amount);
            if (!isValidAmount)
            {
                Utility.PrintMessage("Invalid Amount", true);
                return;
            }

            if (amount <= 0 || amount % 100 != 0)
            {
                Utility.PrintMessage("\nInvalid Amount. The amount must be a positive value and in multiples of 100.", true);
                return;
            }

            if (amount > _account.Balance)
            {
                Utility.PrintMessage("\nInsufficient funds.", true);
                return;
            }

            InsertTransaction(_account.AccountNumber, TransactionType.Withdrawal, amount);

            Console.WriteLine("Processing Withdrawal");
            Utility.PrintDotsAnimation();

            _account.Balance -= amount;
            Console.WriteLine("Withdrawal successful.");
            CheckBalance();
        }

        private void InsertTransaction(int userAccountId, TransactionType type, decimal amount, string desc = "")
        {
            var transaction = new Transaction
            {
                UserAccountId = userAccountId,
                Type = type,
                Amount = amount,
                Description = desc
            };

            _transactions.Add(transaction);
        }

        private void ViewTransactions()
        {
            var accountTransactions = _transactions.Where(t => t.UserAccountId == _account.AccountNumber).ToList();

            if (accountTransactions.Count == 0)
            {
                Utility.PrintMessage("No transactions found for this account.");
                return;
            }

            Console.WriteLine("Transaction History:");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-15} | {1,-10} | {2,-15} | {3,-45} |", "TransactionId", "Type", "Amount", "Description");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");

            foreach (var transaction in accountTransactions)
            {
                Console.WriteLine("| {0,-15} | {1,-10} | {2,-15:C2} | {3,-45} |",
                    transaction.TransactionId,
                    transaction.Type,
                    transaction.Amount,
                    transaction.Description);
            }

            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Utility.PressEnterToContinue();
        }

        private void ProcessInternalTransfer(InternalTransfer internalTransfer)
        {
            if (internalTransfer is null) return;

            var recipientAccount = _accounts.FirstOrDefault(a => a.AccountNumber == internalTransfer.RecipientAccountNumber);

            if (recipientAccount == null)
            {
                Utility.PrintMessage("Transfer Failed. Recipient account not found.", true);
                return;
            }

            if (recipientAccount.AccountNumber == _account.AccountNumber)
            {
                Utility.PrintMessage("Transfer Failed. Cannot transfer to yourself", true);
                return;
            };

            if (!string.Equals(recipientAccount.FullName, internalTransfer.RecipientAccountName, StringComparison.OrdinalIgnoreCase))
            {
                Utility.PrintMessage("Transfer Failed. Recipient account name does not match the account number.", true);
                return;
            }

            if (internalTransfer.TransferAmount % 100 != 0)
            {
                Utility.PrintMessage("Amount must be multiples of 100.", true);
                return;
            }

            if (_account.Balance < internalTransfer.TransferAmount)
            {
                Utility.PrintMessage("Transfer Failed. Insufficient funds.", true);
                return;
            }

            _account.Balance -= internalTransfer.TransferAmount;
            recipientAccount.Balance += internalTransfer.TransferAmount;

            InsertTransaction(_account.AccountNumber, TransactionType.Transfer, internalTransfer.TransferAmount,
                $"Transfered {internalTransfer.TransferAmount} To {recipientAccount.AccountNumber} : {recipientAccount.FullName}");

            InsertTransaction(internalTransfer.RecipientAccountNumber, TransactionType.Transfer, internalTransfer.TransferAmount,
                $"Received {internalTransfer.TransferAmount} From {_account.AccountNumber} : {_account.FullName}");

            Utility.PrintMessage($"Transfer of {internalTransfer.TransferAmount:C2} to account {internalTransfer.RecipientAccountNumber} successful.", false);
            CheckBalance();
        }
    }
}
