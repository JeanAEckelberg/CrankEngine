

namespace CrankEngine.Engine
{
    public class Vector2
    {
        public float x { get; set; }
        public float y { get; set; }

        public Vector2()
        {
            x = 0;
            y = 0;
        }

        public static Vector2 Zero()
        {
            return new Vector2(0,0);
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 Multiply(Vector2 vec)
        {
            return new Vector2(this.x * vec.x, this.y * vec.y);
        }

        public Vector2 Add(Vector2 vec)
        {
            return new Vector2(this.x + vec.x, this.y + vec.y);
        }


        public Vector2 Copy()
        {
            return new Vector2(this.x, this.y);
        }
    }
}
