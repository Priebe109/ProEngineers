using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouletteGame
{
    public abstract class Bet
    {
        private readonly string _playerName;
        private readonly uint _amount;

        protected Bet(string name, uint amount)
        {
            _playerName = name;
            _amount = amount;
        }


        public string PlayerName    { get { return _playerName; }   }
        public uint Amount          { get { return _amount;     }   }

        public virtual uint WonAmount(Field field)  { return 0; }
    }

    public class FieldBet : Bet
    {
        private readonly uint _fieldNumber;

        public FieldBet(string name, uint amount, uint fieldNumber) : base(name, amount)
        {
            _fieldNumber = fieldNumber;
        }

        public override uint WonAmount(Field field)
        {
            if (field.Number == _fieldNumber) return 36 * Amount;
            else return 0;
        }

        public override string ToString()
        {
            return string.Format("{0}$ field bet on {1}", Amount, _fieldNumber);
        }
    }

    public class ColorBet : Bet
    {
        private readonly uint _color;

        public ColorBet(string name, uint amount, uint color) : base(name, amount)
        {
            _color = color;
        }

        override public uint WonAmount(Field field)
        {
            if (field.Color == _color) return 2 * Amount;
            else return 0;
        }
        
        public override string ToString()
        {
            string colorString;

            switch (_color)
            {
                case Field.Red: colorString = "red"; break;
                case Field.Black: colorString = "black"; break;
                default: colorString = "green"; break;
            }

            return string.Format("{0}$ color bet on {1}", Amount, colorString);
        }

    }

    public class EvenOddBet : Bet
    {
        private readonly bool _even;

        public EvenOddBet(string name, uint amount, bool even) : base(name, amount)
        {
            _even = even;
        }

        override public uint WonAmount(Field field)
        {
            if (field.Even == _even) return 2 * Amount;
            else return 0;
        }

        public override string ToString()
        {
            string evenOddString = _even ? "even" : "odd";

            return string.Format("{0}$ even/odd bet on {1}", Amount, evenOddString);
        }
    }
}
