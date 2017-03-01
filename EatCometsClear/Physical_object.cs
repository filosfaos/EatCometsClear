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
        protected int minimalMass;
        
        public Physical_object()
        {
            gravityStrength = 1000;
            minimalMass = 0;
        }

        public int GetMass()
        {
            if (this.mass >= minimalMass)
                return this.mass;
            else
                return minimalMass;
        }
    }
}
