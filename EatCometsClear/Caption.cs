using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace EatCometsClear
{
    class Caption : Drawable
    {
        public Text text;
        public int id;
        private RenderWindow window;

        public Caption(Text inscription, int id, RenderWindow window)
        {
            this.text = new Text(inscription);
            this.id = id;
            this.window = window;
        }

        public Caption()
        {

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            window.Draw(this.text);
        }
    }
}
