using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouletteGame
{
    public class RouletteGame
    {
        private Roulette _roulette;
        private bool _roundIsOpen;
        private List<Bet> _bets;

        public RouletteGame(Roulette roulette)
        {
            _bets = new List<Bet>();
            _roulette = roulette;
        }

        public void OpenBets()
        {
            Console.WriteLine("Round is open for bets");
            _roundIsOpen = true;
        }

        public void CloseBets()
        {
            Console.WriteLine("Round is closed for bets");
            _roundIsOpen = false;
        }

        public void PlaceBet(Bet bet)
        {
            if (_roundIsOpen) _bets.Add(bet);
            else throw new RouletteGameException("Bet placed while round closed");
        }

        public void SpinRoulette()
        {
            Console.Write("Spinning...");
            _roulette.Spin();
            Console.WriteLine("Result: {0}", _roulette.GetResult());
        }

        public void PayUp()
        {
            var result = _roulette.GetResult();

            foreach (var bet in _bets)
            {
                var won = bet.WonAmount(result);
                if(won > 0)
                    Console.WriteLine("{0} just won {1}$ on a {2}", bet.PlayerName, won, bet);
            }
        }

    }

    public class RouletteGameException : Exception
    {
        public RouletteGameException(string s) : base(s)
        {
        }
    }
}
