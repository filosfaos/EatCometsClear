using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EatCometsClear
{
    class YesNoDialog :Dialog
    {

        public YesNoDialog(RenderWindow window, List<Button>buttons, string description)
            :base(window,buttons, description)
        {

        }
    }
}
