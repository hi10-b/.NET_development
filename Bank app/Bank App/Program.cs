using System;


namespace assign2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //aesthetics
            //all screens consist of colour change
            //all screens have unique title names for ease
            //warning and commands are in red colour
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetWindowSize(100,20);
            Console.SetBufferSize(100, 80);

            //new user
            Login user = new Login();
            user.GetUser();

            //start program from menu screen
            Screens options = new Screens();
            options.MainMenu();

            Console.WriteLine();

        }
    }

}
