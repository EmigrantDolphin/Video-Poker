using System;

namespace VideoPoker {
    class Program {
        public static void Main(String[] args) {
            int windowWidth;
            int windowHeight;
            if (Console.LargestWindowWidth >= 100)
                windowWidth = 100;
            else
                windowWidth = Console.LargestWindowWidth;

            if (Console.LargestWindowHeight >= 40)
                windowHeight = 40;
            else
                windowHeight = Console.LargestWindowHeight;


            Console.SetWindowSize(windowWidth, windowHeight);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("*********************");
            Console.WriteLine("    Video Poker      ");
            Console.WriteLine();
            Console.WriteLine("[Left Arrow Key]  - Move the focus left");
            Console.WriteLine("[Right Arrow Key] - Move the focus right");
            Console.WriteLine("[Enter]           - Select focused item");
            Console.WriteLine("[Escape]          - Leave the game (once playing)");
            Console.WriteLine();
            Console.WriteLine("Press [Enter] to start");

            while (true) {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                    break;
            }

            Console.Clear();
            Game game = new Game();
            game.Loop();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }




    }
}
