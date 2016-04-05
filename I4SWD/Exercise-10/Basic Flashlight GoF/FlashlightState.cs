using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Flashlight_GoF
{
    public abstract class FlashlightState
    {
        public virtual void HandlePower(Flashlight context)
        {
            
        }
    }
}
