
namespace AtmSystem.Logic
{
    public class AccountLogic
    {
        private List<Account> accounts = AccountInitializer.Instance.Accounts;

        public Account Login(Account inputAccount)
        {
            Account accountToLogin = GetAccountByCardNumber(inputAccount.CardNumber);

            if (accountToLogin != null && accountToLogin.IsLocked)
            {
                Utility.PrintMessage("Account is locked. Please contact customer support.");
                return null;
            }


            if (accountToLogin != null)
            {
                if (accountToLogin.CardPin == inputAccount.CardPin)
                {
                    accountToLogin.TotalLogin = 0;
                    return accountToLogin;
                }
                else
                {
                    IncrementTotalLogin(accountToLogin.CardNumber);
                    CheckAccountLockStatus(accountToLogin);
                }
            }
            else
            {
                Utility.PrintMessage("Invalid card number. Please try again.", true);
            }

            return null;
        }

        private Account GetAccountByCardNumber(string cardNumber)
        {
            return accounts.FirstOrDefault(a => a.CardNumber == cardNumber);
        }

        private void IncrementTotalLogin(string cardNumber)
        {
            Account accountToUpdate = GetAccountByCardNumber(cardNumber);
            if (accountToUpdate != null)
            {
                accountToUpdate.TotalLogin++;
            }
        }

        private void CheckAccountLockStatus(Account account)
        {
            if (account.TotalLogin >= 3)
            {
                account.IsLocked = true;
                Utility.PrintMessage("Too many failed attempts. Account is now locked.");
            }
        }
    }
}
