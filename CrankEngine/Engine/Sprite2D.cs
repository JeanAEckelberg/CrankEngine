
using System.Drawing;

namespace CrankEngine.Engine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public Color Color = Color.White;
        public string Directory = "";
        public string Tag = "";
        public Bitmap Sprite = null;

        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;

            Image Img = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Sprite =  new Bitmap(Img);

            Log.Info($"[SPRITE2D]({Tag}) - Has been registered!");
            CEngine.RegisterSprite(this);
        }

        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D reference, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Image Img = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Sprite = reference.Sprite;

            Log.Info($"[SPRITE2D]({Tag}) - Has been registered!");
            CEngine.RegisterSprite(this);
        }

        public Sprite2D(string Directory)
        {
            this.Directory = Directory;
            this.Tag = "Reference";

            Image Img = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Sprite = new Bitmap(Img);

            Log.Info($"[SPRITE2D]({Tag}) - Has been registered!");
            CEngine.RegisterSprite(this);
        }

        public bool IsColliding(Sprite2D other)
        {
            if(other == null) return false;
            return (this.Position.x < other.Position.x + other.Scale.x &&
                this.Position.x + this.Scale.x > other.Position.x &&
                this.Position.y < other.Position.y+ other.Scale.y &&
                this.Position.y + this.Scale.y <= other.Position.y);
        }

        public bool IsColliding(Shape2D other)
        {
            if (other == null) return false;
            return (this.Position.x < other.Position.x + other.Scale.x &&
                this.Position.x + this.Scale.x > other.Position.x &&
                this.Position.y < other.Position.y + other.Scale.y &&
                this.Position.y + this.Scale.y > other.Position.y);
        }

        public void DestroySelf()
        {
            Log.Info($"[SPRITE2D]({Tag}) - Has been destroyed!");
            CEngine.UnRegisterSprite(this);
        }
    }
}
