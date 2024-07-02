

namespace AtmSystem.UI
{
    public class Screen
    {
        public static void DisplayAppLogo()
        {
            Console.Clear();
            Console.Title = "Another ATM App";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("""
                _  _____ __  __      _    ____  ____  
               / \|_   _|  \/  |    / \  |  _ \|  _ \ 
              / _ \ | | | |\/| |   / _ \ | |_) | |_) |
             / ___ \| | | |  | |  / ___ \|  __/|  __/ 
            /_/   \_\_| |_|  |_| /_/   \_\_|   |_|   
            
            """);

            Console.ForegroundColor = ConsoleColor.White;

        }

        public static void DisplayAccountMenu()
        {
            Console.Clear();
            DisplayAppLogo();
            Console.Title = "Account Menu";
            Console.WriteLine("""
                
                1. Account Balance
                2. Cash Deposite
                3. Withdrawal
                4. Transfer
                5. Transactions
                6. Logout 
                
                """);

            Console.Write(">> ");
        }

        public static Account UserLoginForm()
        {
            var tempAccount = new Account();
            tempAccount.CardNumber = Utility.GetCardNumbebrInput();
            tempAccount.CardPin = short.Parse(Utility.GetMaskedPinInput());

            return tempAccount;
        }

        public static void DisplayLoginProgress()
        {
            Console.WriteLine("\nChecking card number and PIN...");

            Utility.PrintDotsAnimation(12, 250);
            Console.Clear();
        }

        public static void DisplayLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("Account is locked. Please contact customer support.");
            Utility.PressEnterToContinue();
            Environment.Exit(1);
        }
        public static void DisplayWelcomeMessage(Account account)
        {
            Console.Clear();
            Console.WriteLine($"Welcome, {account.FullName}!");
            Utility.PressEnterToContinue();
        }

        public InternalTransfer InternalTransferForm()
        {
            var internalTransfer = new InternalTransfer();

            Console.WriteLine("Please enter Account Number : ");
            bool isValidAccountNumber = int.TryParse(Console.ReadLine(), out int accountNumber);
            if (!isValidAccountNumber)
            {
                Utility.PrintMessage("Invalid Account Numbebr", true);
                Utility.PressEnterToContinue();
                return null;
            }

            string accountName = Utility.GetUserInput("Please enter Account Name");

            Console.WriteLine("Please enter Amount : ");
            bool isValidAmount = decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0;
            if (!isValidAccountNumber)
            {
                Utility.PrintMessage("Invalid Amount", true);
                Utility.PressEnterToContinue();
                return null;
            }

            internalTransfer.RecipientAccountNumber = accountNumber;
            internalTransfer.RecipientAccountName = accountName;
            internalTransfer.TransferAmount = amount;

            return internalTransfer;
        }

    }
}