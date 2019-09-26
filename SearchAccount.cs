using System;
using System.IO;


namespace assign2
{
    class SearchAccount
    {
        public long accountNum;

        public void AccountSearch()
        {

            DrawScreen draw = new DrawScreen();
            draw.TopScreen();
            Console.WriteLine(" Search Account ");
            draw.UnderTitleLine();

            //do until user exits to main menu
            do
            {
                Console.Write("Enter Account Number: ");

                bool accountValid;

                //get a VALID account number 6-8 digits
                do
                {
                    accountNum = Convert.ToInt64(Console.ReadLine());
                    accountValid = CheckInputValidity(accountNum);
                } while (!accountValid);

                //if account exists
                if (CheckAccountExists(accountNum))
                {
                    string path = $"{accountNum}.txt";
                    string[] detail = File.ReadAllLines(path);

                    ClearLineAbove();
                    
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Account details for {0}: ", accountNum);
                    draw.OtherLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    //display account details 
                    foreach (string set in detail)
                    {
                        Console.WriteLine(set);
                    }
                   // return detail;
                }
                //if account doesnt exist display error message
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("there is no file at this account number");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
            } while (Confirmation());
           // return null;
        }

        //check if user wants to search for another account or exit to menu
        public static bool Confirmation()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Do you want to search another account? (Y/N) ");
            Console.WriteLine("Press 'Y' to search again \nPress 'N' to return to main menu");
            Console.ForegroundColor = ConsoleColor.Cyan;

            //if Y pressed clear screen and search
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.Clear();
                return true;
            }
            //if N pressed go to main menu
            else if (Console.ReadKey().Key == ConsoleKey.N)
            {
                Screens.mainMenu();
                return false;
            }
            return false;
        }

        //check account number input is 6-8 digits or display error message
        public static bool CheckInputValidity(long accountNumber)
        {
            if (accountNumber.ToString().Length >= 6 && accountNumber.ToString().Length <=8)
            {
                return true;
            }else{
                Console.WriteLine("Account number must be 6-8 digits.");
                return false;
            }
        }

        //check if account exists
        public static bool CheckAccountExists(long accountNumber)
        {
            string path = $"{accountNumber}.txt";
            if (File.Exists(path))
            {
                return true;
            }
            return false;
        }

        //clears 1 line in console for less info on screen
        static void ClearLineAbove()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

    }


}
