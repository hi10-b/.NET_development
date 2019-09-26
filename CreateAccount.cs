using System;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace assign2
{
    
    public class CreateAccount
    {
        public int amountTotal;
        public string newFirstName;
        public string newLastName;
        public string newAddress;
        public long newPhone;
        public string newEmail;

        public void NewUser()
        {
            DrawScreen draw = new DrawScreen();
            Console.ForegroundColor = ConsoleColor.Cyan;
            bool phoneValid = false;
            bool emailValid = false;
            bool detailsValid = false;
          
            //do until user exits
            do
            {
                draw.TopScreen();

                Console.WriteLine(" Please Fill Out Your Details Below ");

                draw.UnderTitleLine();

                //request users information
                Console.Write(" First Name: ");
                newFirstName = Console.ReadLine();

                Console.Write(" Last Name: ");
                newLastName = Console.ReadLine();

                Console.Write(" New Address: ");
                newAddress = Console.ReadLine();

                //enter a VALID phone number
                //number must contain 10 digits
                do
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(" New Phone: ");
                        newPhone = Convert.ToInt64(Console.ReadLine());

                        phoneValid = CheckPhoneValidity(newPhone);
                    }catch (FormatException)
                    {
                        ClearLineAbove();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("incorrect input");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                } while (!phoneValid);
                
                //enter a VALID email address
                //email must contain '@' and either 'gmail.com' or 'outlook.com' or 'uts.edu.au'
                do
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" New Email: ");
                    newEmail = Console.ReadLine();
                    emailValid = CheckEmailValidity(newEmail);
                } while (!emailValid);

                //get user confirmation of details
                detailsValid = ConfirmDetail(newFirstName, newLastName, newAddress, newPhone, newEmail);

            } while (!detailsValid);

            //if all details are confirmed correct then create new account file and send email
            if (ConfirmDetail(newFirstName, newLastName, newAddress, newPhone, newEmail))
            {
                AccountFileCreate(newFirstName, newLastName, newAddress, newPhone, newEmail);
                SendEmail(newFirstName, newLastName, newAddress, newPhone, newEmail);
                //Console.ReadKey();
                Screens.mainMenu();
            }
            //else if (ConfirmDetail(newFirstName, newLastName, newAddress, newPhone, newEmail))
            {
              //  Console.WriteLine("go to main menu");
                //Screens restart = new Screens();
                //restart.MainMenu();
            }
        }

        //get user confirmation for data inputted
        static bool ConfirmDetail(string firstName, string lastName, string address, long phoneNum, string email)
        {
            //show data inputted before by user
            Console.Clear();
            Console.WriteLine("First Name: " + firstName);
            Console.WriteLine("Last Name: " + lastName);
            Console.WriteLine("Address: " + address);
            Console.WriteLine("Phone Number: " + phoneNum);
            Console.WriteLine("Email: " + email);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Are these details correct? Y/N " );
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press 'Y' to save \nPress 'N' to reenter details \nPress 'M' to procceed without saving" );
            Console.ForegroundColor = ConsoleColor.Cyan;

            //if Y pressed detail is confirmed
            //if N pressed reenter details
            //if M pressed go to main menu without saving
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.Clear();
                return true;
            }
            else if (Console.ReadKey().Key == ConsoleKey.N)
            {
                Console.Clear();
                return false;
            }
            else if (Console.ReadKey().Key == ConsoleKey.M)
            {
                Console.Clear();
                Screens.mainMenu();
            }
            return false;
        }

        //confirms validity of users phone input
        static bool CheckPhoneValidity(long phoneNum)
        {
            //number must contain 10 digits
            if ((phoneNum.GetType() == typeof(long)) && phoneNum.ToString().Length == 10)
            {
                return true;
            }else 
            //return error message if non valid number input
            {
                ClearLineAbove();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Phone number must contain exactly 10 digits.");
                return false;
            }
        }

        //confirms validity of users email input
        static bool CheckEmailValidity(string emailEntered)
        {
            //email must contain '@' and either 'gmail.com' or 'outlook.com' or 'uts.edu.au'
            if (emailEntered.Contains("@") && (emailEntered.Contains("gmail.com") || emailEntered.Contains("uts.edu.au") || emailEntered.Contains("outlook.com")))
            {
                return true;
            }
            //return error message if non valid email input
            {
                ClearLineAbove();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Email must contain '@' and must be of 'gmail.com' or 'outlook.com' or 'uts.edu.au'");
                return false;
            }
        }

        //clears 1 line in console for less info on screen
        static void ClearLineAbove()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        //creates new account file
        public void AccountFileCreate(string firstName, string lastName, string address, long phoneNum, string email)
        {
            DrawScreen draw = new DrawScreen();

            //create unique account number
            Random randNum = new Random((int)DateTime.Now.Ticks);
            int accountNumber = randNum.Next(111111, 99999999);
           
            //create file named 'account number' and add data to file 
            StreamWriter file = new StreamWriter(accountNumber + ".txt");

            file.WriteLine("First Name: " + firstName);
            file.WriteLine("Last Name: " + lastName);
            file.WriteLine("Address: " + address);
            file.WriteLine("Phone Number: " + phoneNum);
            file.WriteLine("Email: " + email);
            file.WriteLine("Amount: " + amountTotal);

            file.Close();
            draw.TopScreen();

            //show confirmation
            Console.WriteLine(" Congratulations, Your account has been created. ");
            draw.UnderTitleLine();
            Console.WriteLine(" Your account number is: " + accountNumber);
        }

        //send email with all details
        static void SendEmail(string firstName, string lastName, string address, long phoneNum, string email)
        {
            string message = "Hi " + firstName + "\nCongrats!! Your new account has been created:" + "\nFirst Name: " + firstName + "\nLast Name: " + lastName + "\nAddress: " + address + "\nPhone: " + phoneNum + "\nEmail: " + email;
            try
            {
                var client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    Credentials = new NetworkCredential("A.gh1976@outlook.com", "London_123"),
                    EnableSsl = true
                };

                client.Send("A.gh1976@outlook.com", email, "test", message);
                Console.WriteLine("Email has been sent to you");
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("something has gone wrong");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
        }

    }
}
