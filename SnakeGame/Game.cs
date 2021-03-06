using System;
using System.Collections.Generic;
using System.Threading;
using SFML.Graphics;
using SFML.System; 
using SFML.Window;
using SnakeGame.Properties;

namespace SnakeGame
{
    public class Game
    {
        public const int cellSize = 10;
        Snake snake = new Snake(cellSize);
        RenderWindow window = new RenderWindow(new VideoMode(50*cellSize, 50*cellSize), "Game window");
        private bool isPaused = false;
        private bool pausePressed;
        private List<Food> AllFood = new List<Food>();
        private ThreadManager threadManager = new ThreadManager();

        public void Animation()
        {
            Clock clock = new Clock();
            while (window.IsOpen)
            {
                if (clock.ElapsedTime.AsSeconds() > 0.1f)
                {
                    clock.Restart();
                    Random random = new Random();
                    Color color =new Color((byte)random.Next(0,256),(byte)random.Next(0,256),(byte)random.Next(0,256));
                    foreach (CircleShape circle in snake.allSnake)
                    {
                        circle.FillColor = color;
                    }
                }
            }
        }
        private void CreateFood()
        {
            while (AllFood.Count <=1)
            {
                Food food = new Food(cellSize);
                AllFood.Add(food);
            }
        }
        private bool IsIntersecting()
        {
            float snakeX = snake.allSnake[0].Position.X;
            float foodX = AllFood[0].circleShape.Position.X;
            float snakeY = snake.allSnake[0].Position.Y;
            float foodY = AllFood[0].circleShape.Position.Y;
            return foodX < snakeX + snake.allSnake[0].Radius &&
                   foodX > snakeX - snake.allSnake[0].Radius &&
                   foodY < snakeY + snake.allSnake[0].Radius &&
                   foodY > snakeY - snake.allSnake[0].Radius;
        }

        private void DeleteEatedAndIncrese()
        {
            if(IsIntersecting())
            {
                snake.allSnake.Add(snake.SetCircle(snake.allSnake[snake.allSnake.Count-1].Position));
                AllFood.RemoveAt(0);
            }
        }
        void DrawFood()
        {
            foreach (Food food in AllFood)
            {
                window.Draw(food.circleShape);
            }
        }
        public void Start()
        {
            threadManager.InitNewThread(this);
            window.Closed += WindowClosed;
            pausePressed = false;
            while (window.IsOpen && IsNotEnd())
            {
                window.Clear();
                DrawSnake();
                CreateFood();
                DrawFood();
                DeleteEatedAndIncrese();
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