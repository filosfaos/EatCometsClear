using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EatCometsClear
{
    class Drawabable : IDable, Drawable
    {
       // bool enableDrawing;

        public Drawabable()
        {
            //enableDrawing = true;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            throw new NotImplementedException();
        }
    }
}
