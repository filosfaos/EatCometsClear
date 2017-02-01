using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace EatCometsClear
{
    class StaticImage :Drawable
    {
        RenderWindow window;
        Sprite image;

        public StaticImage( RenderWindow window, Sprite image )
        {
            this.window = window;
            this.image = image;
        }

        public void Draw()
        {
            window.Draw(image);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            this.Draw();
        }
    }
}
