using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EatCometsClear
{
    abstract class Dialog
    {
        RenderWindow window;
        RectangleShape background;
        RectangleShape dialog;
        Text description;

        List<Button> buttons;

        public Dialog(RenderWindow window, List<Button> buttons, string descrpition)
        {
            this.window = window;
            this.background = new RectangleShape(new Vector2f(window.Size.X, window.Size.Y));
            this.background.FillColor = new Color(31, 31, 31, 128);
            this.background.Position = new Vector2f(0, 0);

            this.description = new Text(descrpition, new Font("fonts/arial.ttf"));

            this.buttons = new List<Button>();
            foreach (Button element in buttons)
            {
                this.buttons.Add(element);
            }


            int x, y;
            x = (int)window.Size.X / 2;
            y = this.buttons.Count * 100;
            y += 100;
            this.dialog = new RectangleShape();
            this.dialog.Size = new Vector2f(500, 300);
            x = (int)window.Size.X / 2;
            x -= (int)dialog.Size.X / 2;
            y = (int)window.Size.Y / 2;
            y -= (int)dialog.Size.Y / 2;
            this.dialog.Position = new Vector2f(x, y);
            this.dialog.FillColor = new Color(127, 64, 1, 200);

            for (int i = 0; i < this.buttons.Count; i++)
            {
                this.buttons[i].SetPosition(x+ 50, y + 50 + i*100);
            }

            this.description.Position = new Vector2f(dialog.Position.X + 200, dialog.Position.Y + 100);
        }

        public List<Button> GetButtons()
        {
            return buttons;
        }

        public void Draw()
        {
            window.Draw(this.background);
            window.Draw(this.dialog);

            foreach (Button element in buttons)
            {
                element.Draw();
            }

            window.Draw(description);
        }



    }
}
