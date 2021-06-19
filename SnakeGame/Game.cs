using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SnakeGame
{
    public class Game
    {
        public const int cellSize = 10;
        Snake snake = new Snake(cellSize);
        RenderWindow window = new RenderWindow(new VideoMode(50*cellSize, 50*cellSize), "Game window");
        private bool isPaused = false;
        private bool pausePressed;

        public void Start()
        {
            window.Closed += WindowClosed;
            pausePressed = false;
            while (window.IsOpen && IsNotEnd())
            {
                window.Clear();
                DrawSnake();
                if (!isPaused)
                {
                    snake.ChangeSnakePosition();
                }
                StopGame();
                window.DispatchEvents();
                window.Display();
            }
        }

        private void StopGame()
        {
            bool newPausePressed = Keyboard.IsKeyPressed(Keyboard.Key.Space);
            if (newPausePressed!=pausePressed && newPausePressed)
            {
                isPaused =!isPaused;
            }
            pausePressed = newPausePressed;
        }
        private bool IsNotEnd()
        {
            bool flag = false;
            Vector2f newHeadCoord = snake.allSnake[0].Position + snake.direction;
            if (newHeadCoord.X > 0 && newHeadCoord.X < cellSize * 50)
                flag = true;
            else flag = false;
            if (newHeadCoord.Y > 0 && newHeadCoord.Y < cellSize * 50 && flag)
                flag = true;
            else flag = false;
            return flag;
        }
        private void DrawSnake()
        {
            foreach (CircleShape circle in snake.allSnake)
            {
                window.Draw(circle);
            }
        }
        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}