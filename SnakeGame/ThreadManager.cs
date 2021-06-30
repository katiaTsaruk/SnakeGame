using System.Threading;

namespace SnakeGame.Properties
{
    public class ThreadManager
    {
        public void InitNewThread(Game game)
        {
            Thread animThread = new Thread(new ThreadStart(game.Animation));
            animThread.Start();
        }
    }
}