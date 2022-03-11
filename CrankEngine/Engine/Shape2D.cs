using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CrankEngine.Engine
{
    public class Shape2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public Color Color = Color.White;
        public string Tag = "";

        public Shape2D(Color Color, Vector2 Position, Vector2 Scale, string Tag)
        {
            this.Color = Color;
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Log.Info($"[SHAPE2D]({Tag}) - Has been registered!");
            CEngine.RegisterShape(this);
        }

        public void IsColliding()
        {

        }

        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D]({Tag}) - Has been destroyed!");
            CEngine.UnRegisterShape(this);
        }
    }
}
