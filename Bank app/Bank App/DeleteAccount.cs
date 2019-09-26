using System;
using System.IO;


namespace assign2
{
    class DeleteAccount
    {
        public void AccountDelete()
        {
            DrawScreen draw = new DrawScreen();
            draw.TopScreen();
            Console.WriteLine(" Delete Account ");
            draw.UnderTitleLine();

            long accountNum = 0;
            bool accountValid;

            Console.Write("Enter account number: ");
            //get input 6-8 digits account number
            do
            {
                accountNum = Convert.ToInt64(Console.ReadLine());
                accountValid = SearchAccount.CheckInputValidity(accountNum);
            } while (!accountValid);

            //check if account exists with number provided
            bool existingAccount = SearchAccount.CheckAccountExists(accountNum);
            //if exists prompt account deletion
            if (existingAccount)
            {
                string path = $"{accountNum}.txt";
                string[] detail = System.IO.File.ReadAllLines(path);

                //display account details
                foreach (string set in detail)
                {
                    Console.WriteLine(set);
                }
                //get user confirmation to delete
                //if confirmed delete file from path and return to menu
                if (ConfirmDelete())
                {
                    File.Delete(path);
                    Screens.mainMenu();
                }
                //ask if user wants to search again or return to menu if they dont delete previous file
                else
                {
                    CheckAnother();
                }
            }
            else
            {
                Console.WriteLine("Account does not exist ");
                Screens.mainMenu();
            }
            
        }

        //check if user wants to check and delete another account
        static void CheckAnother()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Do you want to search another account? ");
            Console.WriteLine("Press 'Y' to search again ");
            Console.WriteLine("Press 'N' to return to menu ");
            Console.ForegroundColor = ConsoleColor.Cyan;

            //if yes start deletion process again from beginning
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.Clear();
                DeleteAccount restart = new DeleteAccount();
                restart.AccountDelete();
            }
            //else return to main menu
            else if (Console.ReadKey().Key == ConsoleKey.N)
            {
                Screens.mainMenu();
            }
            Screens.mainMenu();
        }

        //confirm to delete file or not
        static bool ConfirmDelete()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAre you sure you want to delete this account ");
            Console.WriteLine("Press 'Y' to confirm ");
            Console.WriteLine("Press 'N' to discard ");
            Console.ForegroundColor = ConsoleColor.Cyan;

            //return true if Y else false
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                return true;
            }
            else if (Console.ReadKey().Key == ConsoleKey.N)
            { 
                return false;
            }
            return false;

        }

    }

}
