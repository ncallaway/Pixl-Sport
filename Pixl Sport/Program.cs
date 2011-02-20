using System;

namespace Pixl_Sport
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (PixlGame game = new PixlGame()) {
                game.Run();
            }
        }
    }
}

