using System;
using SFML.Graphics;
using SFML.System;

namespace SnakeGame
{
    public class Food
    {
        public CircleShape circleShape = new CircleShape();
        private int startRadius = 4;
        private int cellSize;

        public Food(int cellSize)
        {
            this.cellSize = cellSize;
            SetFood();
        }
        private void SetFood()
        {
            Random random = new Random();
            circleShape.Radius = startRadius;
            circleShape.Position=new Vector2f(random.Next(0,50*cellSize-startRadius),random.Next(0,50*cellSize-startRadius));
            circleShape.Origin = new Vector2f(startRadius/2, startRadius/2);
            circleShape.FillColor=new Color((byte)random.Next(0,256),(byte)random.Next(0,256),(byte)random.Next(0,256));
        }
    }
}