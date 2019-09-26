using System;
using System.IO;


namespace assign2
{
    class Withdraw
    {
        // CreateAccount accountNo = new CreateAccount();

        public void WithdrawMoney()
        {
            DrawScreen draw = new DrawScreen();
            draw.TopScreen();
            Console.WriteLine(" Withdrawal ");
            draw.UnderTitleLine();

            long withdrawalAmount;
            long accountNum = 0;
            bool accountValid =false;
            long finalAmount = 0;
            string firstName = null;
            string lastName = null;
            string address = null;
            string phone = null;
            string email = null;

            //get VALID account number 
            Console.Write("Enter account number: ");
            do
            {
                accountNum = Convert.ToInt64(Console.ReadLine());
                accountValid = SearchAccount.CheckInputValidity(accountNum);
            } while (!accountValid);

            //check if account exists with number provided
            bool existingAccount = SearchAccount.CheckAccountExists(accountNum);
            //if account exists prompt withdrawal money
            if (existingAccount)
            {
                Console.Write("Enter withdrawal amount: ");
                withdrawalAmount = Convert.ToInt64(Console.ReadLine());

                string path = $"{accountNum}.txt";
                string[] detail = System.IO.File.ReadAllLines(path);
                bool withdrawn = false;

                // save all other details
                //add to final amount if it already exists
                foreach (string set in detail)
                {
                    string[] splits = set.Split(':');
                    if (set.Contains("First Name"))
                    {
                        firstName = splits[1];
                    }
                    if (set.Contains("Last Name"))
                    {
                        lastName = splits[1];
                    }
                    if (set.Contains("Address"))
                    {
                        address = splits[1];
                    }
                    if (set.Contains("Phone Number"))
                    {
                        phone = splits[1];
                    }
                    if (set.Contains("Email"))
                    {
                        email = splits[1];
                    }
                    //if there is previous value minus withdraw amount and set withdraw to true
                    if (set.Contains("Amount"))
                    {
                        withdrawn = true;
                        finalAmount = (Convert.ToInt64(splits[1]) - withdrawalAmount);
                    }
                }

                //if withdraw amount more then funds in account show error message
                if (finalAmount < 0 || !withdrawn)
                {
                    Console.WriteLine("You do not have suffecient funds in your account!!");
                    Screens.mainMenu();
                }
                //if enough funds edit file
                else
                {
                    EditFile(firstName, lastName, address, phone, email, finalAmount, withdrawalAmount, accountNum, detail);
                    Screens.mainMenu();
                }
            }
            //if no file show error message and go to menu
            else
            {
                Console.WriteLine("Account does not exist ");
                Screens.mainMenu();
            }
        }

        //make changes to file with new transaction
        public void EditFile(string fName, string lName, string address, string phone, string email, long ttlAmount,long withDrawAmt, long accountNum, string[] allDetails)
        {
            DrawScreen draw = new DrawScreen();

            StreamWriter file = new StreamWriter(accountNum + ".txt");
            Console.WriteLine("Account number: " + accountNum);

            //save all details to file and change total amount where requires
            for (int x = 0; x < allDetails.Length; x++)
            {
                //if there is amount then change the total
                if (allDetails[x].Contains("Amount"))
                {
                    file.WriteLine("Amount: " + ttlAmount);
                }
                else
                    file.WriteLine(allDetails[x]);
            }

            //add transactions with deposits to bottom of the file naming it appropriately
            file.Write("Transaction No. {0}: ", (allDetails.Length - 6));
            withDrawAmt = withDrawAmt * -1;
            file.WriteLine($"{withDrawAmt}");
            file.Flush();
            file.Close();


            draw.TopScreen();
            Console.WriteLine(" You have ${0} in your account. ", ttlAmount);
            draw.UnderTitleLine();

          //  return 0;
        }
        
    }
}
