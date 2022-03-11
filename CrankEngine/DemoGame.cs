using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrankEngine.Engine;
using System.Drawing;
using System.Windows.Forms;

namespace CrankEngine
{
    internal class DemoGame : Engine.CEngine
    {
        Sprite2D Player;
        // Shape2D Player;

        Vector2 LastPos;

        bool Left = false;
        bool Right = false;
        bool Up = false;
        bool Down = false;


        string[,] Map =
        {
            {"g","g","g","g","g","g","g","g"},
            {"g",".",".",".",".",".",".","g"},
            {"g",".",".",".",".",".",".","g"},
            {"g",".",".",".","g","g",".","g"},
            {"g",".",".",".",".","g",".","g"},
            {"g",".",".",".",".","g",".","g"},
            {"g",".",".",".",".","g",".","g"},
            {"g","g","g","g","g","g","g","g"},
        };

        Vector2 TileSize = new Vector2(50,50);

        public DemoGame() : base(new Vector2(640, 480), "CrankEngine Demo") { }

        public override void OnLoad()
        {
            //Object load values called here before first frame is rendered.
            BackgroundColor = Color.Black;

            // CameraPosition.x = -100;
            // CameraAngle = 0f;
            CameraZoom = new Vector2(1.25f, 1.25f);

            new Sprite2D(TileSize.Multiply(new Vector2(1, 4)), new Vector2(40,40), "characters","Player");

            //Shape2D ground = new Shape2D(Color.Blue, new Vector2(0, 400), new Vector2(640, 80), "Ground");
            // new Shape2D(Color.Red, new Vector2(10, 10), new Vector2(10, 10), "Player");
        
        
            for(int i = 0; i < Map.GetLength(0); i++)
            {
                for(int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i,j] == "g")
                    {
                        new Shape2D(Color.DarkMagenta,TileSize.Multiply(new Vector2(j,i)), TileSize, "Ground");
                    }
                }
            }
        
        }

        public override void OnDraw()
        {
            
        }

        public override void OnUpdate()
        {
            // CameraPosition.x+=10*Time.DeltaTime;
            // CameraAngle += 0.01f;
            Player = CEngine.GetSpriteWithTag("Player");
            //if (Player != null)
            //{
            //    Player.Position.x++;
            //    if (Time.RunTime > 8000)
            //    {
            //        Player.DestroySelf();
            //    }
            //}

            LastPos = Player.Position.Copy();
            if (Up)
            {
                Player.Position.y -= 100 * Time.DeltaTime;
            }
            if (Left)
            {
                Player.Position.x -= 100 * Time.DeltaTime;
            }
            if (Down)
            {
                Player.Position.y += 100 * Time.DeltaTime;
            }
            if (Right)
            {
                Player.Position.x += 100 * Time.DeltaTime;
            }


            foreach (Shape2D shape in CEngine.GetShapesWithTag("Ground"))
            {
                if (!Player.IsColliding(shape)) continue;
                Player.Position = LastPos.Copy();
            }
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { Up = true; }
            if (e.KeyCode == Keys.A) { Left = true; }
            if (e.KeyCode == Keys.S) { Down = true; }
            if (e.KeyCode == Keys.D) { Right = true; }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { Up = false; }
            if (e.KeyCode == Keys.A) { Left = false; }
            if (e.KeyCode == Keys.S) { Down = false; }
            if (e.KeyCode == Keys.D) { Right = false; }
        }
    }
}
