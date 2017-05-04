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
        protected string gameTitle;

        public void CloseWindow()
        {
            window.Close();
        }

        public void CreateWindow(string title)
        {
            uint height = Convert.ToUInt32( System.Configuration.ConfigurationManager.AppSettings["screenY"]);
            uint width = Convert.ToUInt32(System.Configuration.ConfigurationManager.AppSettings["screenX"]);
            string style = System.Configuration.ConfigurationManager.AppSettings["WindowMode"];

            if (window != null)
            {
                window.Close();
                window = null;
            }

            if(style.Equals("window"))
            {
                this.window = new RenderWindow(new VideoMode(width, height), title, Styles.Default);
            }
            else if (style.Equals("full"))
            {
                this.window = new RenderWindow(new VideoMode(width, height), title, Styles.Fullscreen);
            }
            else
            {
                this.window = new RenderWindow(new VideoMode(800, 600), title, Styles.Close);
            }


            this.window.SetFramerateLimit(60);

            //set up events
            window.Closed += Window_Closed;
        }

        public Game(string title, Color clearColor)
        {
            gameTitle = title;
            this.clearColor = clearColor;
        }



        public void Run()
        {
            bool isLoaded = false;

            Task loading = new Task( () =>
            {
                uint i = 1;


                Console.Write("Starting ");
                string a = "/";

                while (!isLoaded)
                {
                    switch (i%4)
                    {
                        case 0:
                            a = "/";
                            break;
                        case 1:
                            a = "-";
                            break;
                        case 2:
                            a = "\\";
                            break;
                        case 3:
                            a = "|";
                            break;
                    }

                    if (i == 16)
                    {
                        i = 0;
                        Console.Clear();
                        Console.Write("Starting ");
                    }

                    Console.CursorLeft = 10;

                    i++;
                    
                    Console.Write(a);
                    System.Threading.Thread.Sleep(50);
                }

                Console.Clear();
                Console.WriteLine("Game is ready!");
            } );

            loading.Start();

            LoadContent();
            Initialize();

            isLoaded = true;

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

        protected abstract void Window_resized(object sender, EventArgs e);
        private void Window_Closed(object sender, EventArgs e)
        {
            window.Close();
        }
        
    }
}
