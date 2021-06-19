using SFML.Graphics;

namespace SnakeGame
{
    public class Food
    {
        private CircleShape circleshape = new CircleShape();
        

        public Food()
        {
            
        }
        private void SetFood()
        {
            circleshape.Radius = 4;
            
        }
    }
}