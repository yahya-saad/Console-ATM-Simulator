AccountLogic accountService = new AccountLogic();

while (true)
{
    Screen.DisplayAppLogo();

    Account inputAccount = Screen.UserLoginForm();

    Screen.DisplayLoginProgress();

    Account loggedInAccount = accountService.Login(inputAccount);

    if (loggedInAccount != null)
    {
        Screen.DisplayWelcomeMessage(loggedInAccount);
        Screen.DisplayAccountMenu();

        AccountMenuHandler menuHandler = new AccountMenuHandler(loggedInAccount);
        bool continueSession = true;

        while (continueSession)
        {
            Screen.DisplayAccountMenu();

            bool isValidOption = int.TryParse(Console.ReadLine(), out int option);
            if (!isValidOption)
            {
                Utility.PrintMessage("Invalid Option..please try again", true);
            }
            else if ((AccountMenuOptions)option == AccountMenuOptions.Logout)
            {
                continueSession = false;
                Console.Write("Logging out...");
            }
            else
            {
                menuHandler.HandleCommand(option);
            }
        }
        Utility.PrintDotsAnimation(20);
        Utility.PressEnterToContinue();
        Thread.Sleep(500);
    }
}

