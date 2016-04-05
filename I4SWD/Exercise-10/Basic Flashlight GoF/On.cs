using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Flashlight_GoF
{
    public class On: FlashlightState
    {
        public override void HandlePower(Flashlight context)
        {
            context.LightOff();
            context.SetState(new Off());
        }
    }
}
