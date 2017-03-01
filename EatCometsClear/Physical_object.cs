using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Window;

namespace EatCometsClear
{
    abstract class Physical_object
    {

        public int mass;
        public Vector2f position;
        public int gravityStrength;
        public bool enableGravity;
    }
}
