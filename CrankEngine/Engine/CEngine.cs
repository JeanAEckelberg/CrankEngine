using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace CrankEngine.Engine
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }


    public abstract class CEngine
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "New Game";
        private Canvas Window = null;
        public long Frame { get; set; }
        private Thread GameLoopThread = null;


        private static List<Shape2D> AllShapes = new List<Shape2D>();
        private static List<Sprite2D> AllSprites = new List<Sprite2D>();


        public Color BackgroundColor = Color.Crimson;


        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0f;
        public Vector2 CameraZoom = new Vector2(1, 1);

        public CEngine(Vector2 ScreenSize, string Title)
        {
            Log.Info("Game is starting...");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)ScreenSize.x, (int)ScreenSize.y);
            Window.Text = this.Title;
            Window.Paint += Renderer;

            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;

            Window.FormBorderStyle = FormBorderStyle.FixedSingle;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }

        public static Shape2D GetShapeWithTag(string name)
        {
            var TempList = from shape in AllShapes
                           where shape.Tag.Equals(name)
                           select shape;
            return TempList.FirstOrDefault();
        }

        public static List<Shape2D> GetShapesWithTag(string name)
        {
            var TempList = from shape in AllShapes
                           where shape.Tag.Equals(name)
                           select shape;
            return TempList.ToList();
        }

        public static Sprite2D GetSpriteWithTag(string name)
        {
            //Option 1
            //foreach(Sprite2D sprite in AllSprites)
            //{
            //    if(sprite.Tag.Equals(name)) return sprite;
            //}
            //return null;

            //Option 2
            //var TempList = from sprite in AllSprites
            //               where sprite.Tag.Equals(name)
            //               select sprite;
            //return TempList.FirstOrDefault();
            
            //Option 3
            return AllSprites.Find(sprite => sprite.Tag.Equals(name));
        }

        public static List<Sprite2D> GetSpritesWithTag(string name)
        {
            var TempList = from sprite in AllSprites
                           where sprite.Tag.Equals(name)
                           select sprite;
            return TempList.ToList();
        }

        public static void UnRegisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }

        public static void RegisterSprite(Sprite2D sprite)
        {
            AllSprites.Add(sprite);
        }

        public static void UnRegisterSprite(Sprite2D sprite)
        {
            AllSprites.Remove(sprite);
        }

        void GameLoop()
        {
            Frame = 0;
            Time.StartClock();
            OnLoad();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    Time.TickTime();
                    OnUpdate();
                    Frame++;
                    Thread.Sleep(1);
                }
                catch
                {
                    Log.Error("Game has not been found...");
                }
            }
        }

        private void Renderer(Object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.TranslateTransform(-CameraPosition.x, -CameraPosition.y);
            g.RotateTransform(CameraAngle);
            g.ScaleTransform(CameraZoom.x,CameraZoom.y);

            g.Clear(BackgroundColor);

            foreach(Shape2D shape in AllShapes)
            {
                g.FillRectangle(new SolidBrush(shape.Color), shape.Position.x, shape.Position.y, shape.Scale.x, shape.Scale.y);
            }
            foreach(Sprite2D sprite in AllSprites)
            {
                if (sprite.Tag == "Reference") continue;
                g.DrawImage(sprite.Sprite,sprite.Position.x,sprite.Position.y, sprite.Scale.x, sprite.Scale.y);
            }
        }

        public abstract void OnLoad();

        public abstract void OnUpdate();

        public abstract void OnDraw();

        public abstract void GetKeyDown(KeyEventArgs e);

        public abstract void GetKeyUp(KeyEventArgs e);
    }
}
