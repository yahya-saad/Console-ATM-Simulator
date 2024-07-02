
using AtmSystem.Data.Entities;

namespace AtmSystem.UI
{
    public static class Utility
    {
        public static void PressEnterToContinue()
        {
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        public static string GetUserInput(string input)
        {
            Console.WriteLine(input);
            return Console.ReadLine();
        }

        public static void PrintMessage(string message, bool isError = false)
        {
            if (isError) Console.ForegroundColor = ConsoleColor.Red;
            else Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(message);
            Console.ResetColor();
            PressEnterToContinue();
        }

        public static void PrintDotsAnimation(int length = 15, int time = 200)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write(".");
                Thread.Sleep(time);
            }
            Console.WriteLine();
        }

        public static string GetCardNumbebrInput()
        {
            bool isValidCardNumber = false;
            string cardnumber = "";

            while (!isValidCardNumber)
            {
                cardnumber = Utility.GetUserInput("Enter card number (8 digits)");
                isValidCardNumber = cardnumber.Length == 8 && cardnumber.All(char.IsDigit); ;

                if (!isValidCardNumber)
                {
                    Utility.PrintMessage("Invalid Card Number...", true);
                    Thread.Sleep(300);
                }
            }

            return cardnumber;
        }
        public static string GetMaskedPinInput()
        {
            const int pinLength = 4;
            StringBuilder pinBuilder = new StringBuilder();

            Console.Write("Enter PIN (4 digits): ");

            while (pinBuilder.Length < pinLength)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Backspace && pinBuilder.Length > 0)
                {
                    pinBuilder.Remove(pinBuilder.Length - 1, 1);
                    Console.Write("\b \b"); // Clear the last asterisk and move cursor back
                }
                else if (char.IsDigit(key.KeyChar) && pinBuilder.Length < pinLength)
                {
                    pinBuilder.Append(key.KeyChar);
                    Console.Write("*");
                }
            }

            Console.WriteLine();
            return pinBuilder.ToString();
        }
    }
}
