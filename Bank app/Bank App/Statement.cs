using System;
using System.Net.Mail;
using System.Net;

namespace assign2
{
    class Statement
    {
        public void LastFiveTransaction()
        {
            DrawScreen draw = new DrawScreen();
            draw.TopScreen();
            Console.WriteLine(" A/C Statement ");
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
            //if account exists prompt withdrawal money
            if (existingAccount)
            {
                Console.Clear();
                Console.WriteLine("Account Number: " + accountNum);

                string path = $"{accountNum}.txt";
                //all details from file
                string[] detail = System.IO.File.ReadAllLines(path);
                //last five transaction details
                string[] lastFive = new string[5];
                int y = 0;
                draw.OtherLine();
                //look at bottom five lines in file where all transactions are saved
                Console.WriteLine("upto 5 previous transactions shown: ");
                
                for (int x=(detail.Length-5); x<detail.Length; x++){
                    //only add transaction if it contains "transaction"
                    if (detail[x].Contains("Transaction"))
                    { 
                        Console.WriteLine(detail[x]);
                        lastFive[y] = detail[x];
                        y++;
                    }
                }

                //check if user wants statement sent to email
                //YesNoEmail();
                //if user wants email statement then email last five trasnactions to their email
                draw.OtherLine();
                if (YesNoEmail())
                {
                    Console.WriteLine("Please enter your email address: ");
                    string email = Console.ReadLine();
                    EmailStatement(lastFive, email);
                    Screens.mainMenu();
                }
                //if no email then go to menu
                else
                {
                    Screens.mainMenu();
                }
            }
            //if account doesnt exist go to menu after message shown
            else
            {
                Console.WriteLine("Account does not exist ");
                Console.ReadKey();
                Screens.mainMenu();
            }
        }

        //check if user wants an email statement or not
        static bool YesNoEmail()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press Y to send email");
            Console.WriteLine("Press N to not send email");
            Console.ForegroundColor = ConsoleColor.Cyan;
            //if Y pressed then true else false
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.Clear();
                return true;
            }
            else return false;
        }

        //send email with the last five transactions
        static void EmailStatement(string[] lastTransactions, string email)
        {
            //array of transaction to string
            string trans1, trans2, trans3, trans4, trans5;

            trans1 = lastTransactions[0];
            trans2 = lastTransactions[1];
            trans3 = lastTransactions[2];
            trans4 = lastTransactions[3];
            trans5 = lastTransactions[4];

            //create message to be sent in email
            string result = "Upto 5 transactions shown below: " + "\n" + trans1 + "\n" + trans2 + "\n" + trans3 + "\n" + trans4 + "\n" + trans5;
            try
            {
                //send email
                var client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    Credentials = new NetworkCredential("A.gh1976@outlook.com", "London_123"),
                    EnableSsl = true
                };
                //from, to, subject, message(body)
                client.Send("A.gh1976@outlook.com", email, "test", result);
                Console.WriteLine("Email has been sent to you");
                // Console.ReadKey();
            }
            catch (FormatException )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("something has gone wrong");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
        }

        
    }
}
