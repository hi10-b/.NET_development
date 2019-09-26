using System;


namespace assign2
{
    //draws screen to the width of the console
    //Start screen is set to certain width and height for user comfort..not too big or small
    //aethetics
    class DrawScreen
    {
        public void TopScreen()
        {
            //Console.Clear();
            for (int x = 0; x < Console.WindowWidth; x++)
            {
                Console.Write("=");
            }
        }
        public void UnderTitleLine()
        {
            for (int x = 0; x < Console.WindowWidth; x++)
            {
                Console.Write("*");
            }
        }
        public void OtherLine()
        {
            for (int x = 0; x < Console.WindowWidth; x++)
            {
                Console.Write("-");
            }
        }
        public void BottomScreen()
        {
            for (int x = 0; x < Console.WindowWidth; x++)
            {
                Console.Write(".");
            }
        }
    }
}
