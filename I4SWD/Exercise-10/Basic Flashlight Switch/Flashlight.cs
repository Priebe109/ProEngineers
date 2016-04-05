using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Flashlight_Switch
{
    public class Flashlight
    {
        private enum State
        {
            On,
            Off
        }

        private State _currentState;

        public Flashlight()
        {
            _currentState = State.Off;
        }

        public void Power()
        {
            switch (_currentState)
            {
                case State.Off:
                    LightOn();
                    _currentState = State.On;
                    break;
                case State.On:
                    LightOff();
                    _currentState = State.Off;
                    break;
            }
        }

        private void LightOn()
        {
            // turn light on
        }

        private void LightOff()
        {
            // turn light off
        }
    }
}
