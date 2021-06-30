using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SnakeGame
{
    public class Snake
    {
        public List<CircleShape> allSnake = new List<CircleShape>();
        private int cellSize;
        public Vector2f direction;
        
        public Snake(int cellSize)
        {
            this.cellSize = cellSize;
            SetInitialSnake();
        }
        public CircleShape SetCircle(Vector2f position)
        {
            CircleShape circle = new CircleShape();
            circle.Radius = cellSize;
            circle.Origin = new Vector2f(circle.Radius / 2,circle.Radius / 2);
            circle.FillColor=Color.Green;
            circle.Position = position;
            return circle;
        }
        
        public void ChangeSnakePosition()
        {
            SetDirection();
            if (direction != new Vector2f(0, 0))
            {
                Vector2f headPosition = allSnake[0].Position;
                foreach (CircleShape circle in allSnake)
                {
                    if (circle.Position == headPosition)
                    {
                        circle.Position += direction;
                    }
                    else
                    {
                        circle.Position += (headPosition - circle.Position) / allSnake.IndexOf(circle) * 0.004f;
                    }
                }
            }
        }

        private void SetDirection()
        {
            float delta = 0.02f;
            
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                direction=new Vector2f(-delta,0);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                direction=new Vector2f(delta, 0f);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                direction=new Vector2f(0f, -delta);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                direction=new Vector2f(0f, delta);
            }
        }

        private void SetInitialSnake()
        {
            for (int i = 9; i > 0; i--)
            {
                allSnake.Add(SetCircle(new Vector2f(i*cellSize/2,cellSize/2)));
            }
        }
    }
}