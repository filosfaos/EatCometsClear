using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;


namespace EatCometsClear
{
    abstract class Game
    {
        protected RenderWindow window;
        protected Color clearColor;

        public void CloseWindow()
        {
            window.Close();
        }

        public Game(uint width, uint height, string title, Color clearColor)
        {
            this.window = new RenderWindow(new VideoMode(width, height), title, Styles.Close);
            this.window.SetFramerateLimit(60);
            this.clearColor = clearColor;

            //set up events
            window.Closed += Window_Closed;
        }



        public void Run()
        {
            LoadContent();
            Initialize();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                Tick();

                window.Clear(clearColor);
                Render();
                window.Display();

            }
        }

        protected abstract void LoadContent();
        protected abstract void Initialize();

        protected abstract void Tick();
        protected abstract void Render();

        private void Window_Closed(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}
