using System.Threading;
using SFML.Graphics;

namespace SnakeGame
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}