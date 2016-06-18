using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraCreditCardValidator
{
    public class CreditCardValidator
    {
        private const int MinimumNumberOfNamesRequired = 2;
        private readonly string[] _acceptedCreditCardTypes = {"Visa", "MasterCard", "American Express"};

        public bool ValidateName(string name)
        {
            var names = name.Split(' ');

            return
                names.Length >= MinimumNumberOfNamesRequired
                && NameIsNotInitial(names.First())
                && NameIsNotInitial(names.Last());
        }

        private bool NameIsNotInitial(string name)
        {
            return name.Length > 1 && name.Last() != '.';
        }

        public bool ValidateCreditCardType(string type)
        {
            return _acceptedCreditCardTypes.Contains(type);
        }
    }
}
