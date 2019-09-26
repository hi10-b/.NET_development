using System;


namespace assign2
{
    class Screens
    {
        DrawScreen draw = new DrawScreen();
        public void MainMenu() //main menu
        {
            Console.Title = "Main Menu";
            Console.Clear();
            
            Console.ForegroundColor = ConsoleColor.Cyan;

            draw.TopScreen();
            Console.WriteLine(" MAIN MENU ");
            draw.UnderTitleLine();

            //display all available menus
            Console.WriteLine("1. Create new account");
            Console.WriteLine("2. Search for account");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. A/C statement");
            Console.WriteLine("6. Delete account");
            Console.WriteLine("7. Exit");

            draw.BottomScreen();

            Console.Write(" Enter your choice 1-7: ");

            //get user input to traverse through the application
            try
            {
                //switch cases accordingly to user input
                //aesthetics and user friendly names are set accordingly
                //titles are set which lable each screen on top
                //each screen is set to clear when prompted removing all previous trash
                int menuChoice = Int32.Parse(new string(Console.ReadKey().KeyChar, 1));
                Console.WriteLine();
                switch (menuChoice)
                {
                    case 1:
                        Console.Title = "Create new account";
                        Console.Clear();
                        
                        CreateAccount newAccount = new CreateAccount();
                        newAccount.NewUser();
                        //createAccount();
                        break;
                    case 2:
                        Console.Title = "Search Account";
                        Console.Clear();

                        SearchAccount search = new SearchAccount();
                        search.AccountSearch();
                        Console.WriteLine("done and dusted");
                        break;
                    case 3:
                        Console.Title = "Deposit into Account";
                        Console.Clear();

                        Deposit deposit = new Deposit();
                        deposit.DepositMoney();

                        break;
                    case 4:
                        Console.Title = "Withdraw from Account";
                        Console.Clear();

                        Withdraw withdraw = new Withdraw();
                        withdraw.WithdrawMoney();

                        break;
                    case 5:
                        Console.Title = "Account Statement";
                        Console.Clear();

                        Statement lastStatement = new Statement();
                        lastStatement.LastFiveTransaction();

                        break;
                    case 6:
                        Console.Title = "Delete Account";
                        Console.Clear();

                        DeleteAccount accDelete = new DeleteAccount();
                        accDelete.AccountDelete();

                        break;
                    case 7:
                       // Console.WriteLine("Press any key to confirm exit, Thanks!");
                        break;
                }
            }
            //excption message such as letter entered in above numeric switch statement
            catch (FormatException)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong, Press any key to return to menu ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.ReadKey();
                MainMenu();

                Console.ReadKey();
            }
        }

        //when prompted from other screen guides user to menu screen
        public static void mainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress any key to return to Menu ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.ReadKey();
            Screens back = new Screens();
            back.MainMenu();
        }
    }
}
