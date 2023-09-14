using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using Key = SFML.Window.Keyboard.Key;

namespace Simu16
{
    public static class DisplayWindow
    {
        public static Thread?        Thread;
        public static RenderWindow?  Window;
        public static RenderTexture? Target;
        public static Texture?       FontTex;

        public static int FPS { get; private set; }

        private static RectangleShape _rect   = new RectangleShape();
        private static Sprite         _sprite = new Sprite();
        private static Vector2f       _vec    = new Vector2f();
        private static IntRect        _bnds   = new IntRect();
        private static int            _tm, _frames;

        public static void Init()
        {
            Window = null;
            Thread = new Thread(ThreadMain);
            Thread.Start();
        }

        public static void ThreadMain()
        {
            Window = new RenderWindow(new VideoMode(640, 400), "Simu16");
            Window.SetVerticalSyncEnabled(false);
            Window.SetFramerateLimit(0);
            Window.Closed += (sender, args) => Window.Close();
            Window.KeyPressed += OnKeyPress;

            Target = new RenderTexture(640, 400);

            if (!File.Exists("Font.png")) { Debug.Error("Unable to locate font file 'Font.png'"); return; }
            Image img = new Image("Font.png");
            img.CreateMaskFromColor(Color.Black);
            FontTex = new Texture(img);

            _rect   = new RectangleShape();
            _sprite = new Sprite(FontTex, new IntRect(0, 0, 8, 16));
            _vec    = new Vector2f();
            _bnds   = new IntRect(0, 0, 8, 16);

            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                Window.Clear(Color.Black);
                Display(App.Bus.VGA.Mode.TextMode);

                _vec.X = 0; _vec.Y = 0;
                _sprite.Position = _vec;
                _sprite.Texture = Target.Texture;
                _sprite.Color = Color.White;

                _bnds.Left = 0; _bnds.Top = 0; _bnds.Width = (int)Window.Size.X; _bnds.Height = (int)Window.Size.Y;
                _sprite.TextureRect = _bnds;
                Window.Draw(_sprite);

                Window.Display();
            }
        }

        public static void Display(bool txt_mode = true)
        {
            if (Window == null || Target == null || App.Bus == null) { return; }

            _frames++;
            if (_tm != DateTime.Now.Second)
            {
                _tm = DateTime.Now.Second;
                FPS = _frames;
                _frames = 0;
                Window.SetTitle("Simu16 | FPS:" + FPS);
            }

            Target.Clear();

            if (txt_mode)
            {
                byte attr, fg, bg;
                int xx, yy, i;
                for (i = 0; i < 80 * 25; i++)
                {
                    attr = App.Bus.Data[(i * 2) + 1];
                    fg   = (byte)((attr & 0xF0) >> 4);
                    bg   = (byte)((attr & 0x0F));
                    xx   = i % 80;
                    yy   = i / 80;

                    DrawChar(xx * 8, yy * 16, (char)App.Bus.Data[(i * 2)], VGAController.Palette16[fg], VGAController.Palette16[bg]);
                }
            }

            Target.Display();
        }

        public static void DrawChar(int x, int y, char c, Color fg, Color bg)
        {
            if (Window == null || Target == null || FontTex == null) { return; }

            int sx = (c - 32) * 8;

            _vec.X = 8; _vec.Y  = 16;
            _rect.Size          = _vec;

            _vec.X = x; _vec.Y  = y;
            _rect.Position      = _vec;

            _rect.OutlineColor  = Color.Transparent;
            _rect.FillColor     = bg;
            Target.Draw(_rect);

            if (c != 0 && c != 0x20)
            {
                _bnds.Left          = sx; _bnds.Top = 0; _bnds.Width = 8; _bnds.Height = 16;
                _sprite.Position    = _vec;
                _sprite.Texture     = FontTex;
                _sprite.TextureRect = _bnds;
                _sprite.Color       = fg;
                Target.Draw(_sprite);
            }
        }

        public static void OnKeyPress(object? sender, KeyEventArgs e)
        {
            if (App.Bus == null) { return; }
            
            App.Bus.KBD.Buffer.Enqueue(e.Code);
            App.Bus.CPU.InvokeInterrupt(Interrupt.Keyboard);
        }
    }
}
