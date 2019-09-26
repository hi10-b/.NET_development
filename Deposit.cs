using System;
using System.IO;


namespace assign2
{
    class Deposit
    {
        public void DepositMoney()
        {
            DrawScreen draw = new DrawScreen();
            draw.TopScreen();
            Console.WriteLine(" Deposit ");
            draw.UnderTitleLine();

            long depositAmount;
            long accountNum = 0;
            bool accountValid = false;
            long finalAmount = 0;
            string firstName = null;
            string lastName = null;
            string address = null;
            string phone = null;
            string email = null;

            Console.Write("Enter account number: ");
            //get Valid account number 6-8 digits
            do
            {
                accountNum = Convert.ToInt64(Console.ReadLine());
                accountValid = SearchAccount.CheckInputValidity(accountNum);
            } while (!accountValid);

            //check if account exists with number provided
            bool existingAccount = SearchAccount.CheckAccountExists(accountNum);
            //if account exists prompt deposit money
            if (existingAccount)
            {
                Console.Write("Enter deposit amount: ");
                depositAmount = Convert.ToInt64(Console.ReadLine());

                string path = $"{accountNum}.txt";
                string[] detail = File.ReadAllLines(path);
                bool deposited = false;

                //save all other details 
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
                    if (set.Contains("Transaction"))
                    {
                        //make no changes if it contains 'transactions'
                    }
                    if (set.Contains("Amount"))
                    {
                        finalAmount = (Convert.ToInt64(splits[1]) + depositAmount);
                        deposited = true;
                    }                    
                }
                //if amount doesnt exists in final then deposit amount = final amount
                if (!deposited)
                {
                    finalAmount = depositAmount;
                }
                //make appropriate changes to file and exit appropriately
                EditFile(firstName, lastName, address, phone, email, finalAmount,depositAmount, accountNum, detail);
                Screens.mainMenu();
            }
            //if account doesnt exist display error message and go to menu
            else
            {
                Console.WriteLine("Account does not exist ");
                Screens.mainMenu();
            }
        }
        
        //make changes to file with new values
        public int EditFile(string fName, string lName, string address, string phone, string email, long ttlAmount, long depAmount, long accountNum, string[] allDetails)
        {
            DrawScreen draw = new DrawScreen();

            StreamWriter file = new StreamWriter(accountNum + ".txt");
            Console.WriteLine("Account number: " + accountNum);

            //save all details to file and change total amount where requires
            for(int x=0; x<allDetails.Length; x++){
                //if there is amount then change the total
                if (allDetails[x].Contains("Amount"))
                {
                    file.WriteLine("Amount: " + ttlAmount);
                }else
                file.WriteLine(allDetails[x]);
            }
 
            //add transactions with deposits to bpttom of the file naming it appropriately
            file.Write("Transaction No. {0}: ",(allDetails.Length-6) );
            file.WriteLine($"{depAmount}");
            file.Flush();
            file.Close();

            draw.TopScreen();
            Console.WriteLine(" Congratulations, You have ${0} ", ttlAmount);
            draw.UnderTitleLine();

            return 0;
        }
    }
}
