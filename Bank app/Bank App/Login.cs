using System;
using System.IO;


namespace assign2
{
    class Login
    {
        DrawScreen draw = new DrawScreen();

        //get user info
        public void GetUser()
        {
            Console.Title = "Banking App $$";
            draw.TopScreen();
            Console.WriteLine(" WELCOME TO THE BANKING APP ");
            draw.UnderTitleLine();

            string userNameIn;
            string userPassIn;
            bool valid = false;

            //get VALID user login info
            do
            {
                userNameIn = null;
                userPassIn = null;

                Console.WriteLine(" Please enter your credentials: ");

                draw.OtherLine();

                //get user input
                Console.Write(" Username: ");
                userNameIn = Console.ReadLine();

                Console.Write(" Password: ");
                ConsoleKeyInfo info = Console.ReadKey(true);

                //hide password input with *
                while (info.Key != ConsoleKey.Enter)
                {
                    if (info.Key != ConsoleKey.Backspace)
                    {
                        Console.Write("*");
                        userPassIn += info.KeyChar;
                    }
                    else if (info.Key == ConsoleKey.Backspace)
                    {
                        if (!string.IsNullOrEmpty(userPassIn))
                        {
                            userPassIn = userPassIn.Substring(0, userPassIn.Length - 1);
                            int pos = Console.CursorLeft;
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        }
                    }
                    info = Console.ReadKey(true);
                }
                Console.WriteLine();
                //check if password and username input match
                valid = CheckValidity(userNameIn, userPassIn);
            } while (!valid);
        }

        static bool CheckValidity(string userName, string userPass)
        {
            //locate file and split each line with '|'
            //username on left and password on right
            try
            {
                string[] lines = File.ReadAllLines("login.txt");
                foreach (string set in lines)
                {
                    //if password and username input match those in file return true
                    string[] splits = set.Split('|');
                    if (splits[0] == userName && splits[1] == userPass)
                    {
                        return true;
                    }
                }
                Console.Clear();
                DrawScreen draw = new DrawScreen();
                draw.TopScreen();

                //if no correct combination return false
                Console.WriteLine("Incorrect credentials! Please try again!!");

                draw.UnderTitleLine();
                return false;
            }
            //file not found exception
            catch (FileNotFoundException )
            {
                Console.WriteLine("file not found" );
                Console.ReadKey();
                return false;
            }


        }
        
    }
}
