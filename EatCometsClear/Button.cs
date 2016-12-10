using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EatCometsClear
{
    class Button
    {

        float rx, ry, rw, rh;
        RenderWindow window;
        RectangleShape prostokat;
        public Text tekst;
        //Action akcja;
        public bool doAction;
        Color color, activeColor;

        public Button(float posx, float posy, float width, float height, string napis, RenderWindow okno, Color color, uint textsize)
        {

            this.color = color;
            int roznica = -32;
            int r, g, b;

            r = color.R + roznica;
            if (r > 255)
                r = 255;
            if (r < 0)
                r = 0;

            g = color.G + roznica;
            if (g > 255)
                g = 255;
            if (g < 0)
                g = 0;
            b = color.B + roznica;
            if (b > 255)
                b = 255;
            if (b < 0)
                b = 0;

            this.activeColor = new Color((byte)r, (byte)g, (byte)b);


            doAction = false;
            rx = posx;
            ry = posy;
            rw = width;
            rh = height;
            prostokat = new RectangleShape();
            prostokat.Position = new Vector2f(rx, ry);
            prostokat.Size = new Vector2f(rw, rh);
            prostokat.FillColor = new Color(255, 0, 0);
            tekst = new Text();
            tekst.DisplayedString = napis;
            tekst.Font = new Font("fonts/arial.ttf");
            tekst.Position = new Vector2f(rx + (rw - tekst.GetLocalBounds().Width) / 2, ry + (rh - tekst.GetLocalBounds().Height) / 2);
            window = okno;

            if ((napis == "-"))
            {
                tekst.Position = new Vector2f(rx + 15, ry + 2);
            }
            if ((napis == "+"))
            {
                tekst.Position = new Vector2f(rx + 12, ry + 3);
            }
            tekst.CharacterSize = textsize;
        }

        public void ChangeText(String text)
        {
            this.tekst.DisplayedString = text;
            tekst.Position = new Vector2f(rx + (rw - tekst.GetGlobalBounds().Width) / 2, ry + (rh - tekst.GetGlobalBounds().Height) / 2);
        }

        bool IsHoovering()
        {
            float mx = Mouse.GetPosition(window).X;
            float my = Mouse.GetPosition(window).Y;
            if (mx > rx && mx < rx + rw && my > ry && my < ry + rh)
            {
                return true;
            }
            return false;
        }

        public bool DoAction()
        {
            if (doAction == true)
            {
                doAction = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool OnClick()
        {
            doAction = true;
            return true;
        }


        public bool Draw()
        {
            window.Draw(prostokat);
            window.Draw(tekst);

            if (IsHoovering())
            {
                prostokat.FillColor = activeColor;
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    return OnClick();
            }
            else
                prostokat.FillColor = color;
            return true;
        }
    }
}
