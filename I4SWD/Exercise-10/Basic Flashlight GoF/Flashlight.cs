using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Flashlight_GoF
{
    public class Flashlight
    {
        private FlashlightState _currentState;

        public Flashlight()
        {
            _currentState = new Off();
        }

        public void Power()
        {
            _currentState.HandlePower(this);
        }

        public void LightOn()
        {
            // turn on flashlight
        }

        public void LightOff()
        {
            // turn off flashlight
        }

        public void SetState(FlashlightState state)
        {
            _currentState = state;
        }
    }
}
