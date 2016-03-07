namespace DoorControl.Test.Unit
{
    namespace Fakes
    {
        public class FakeValidation: IUserValidation
        {
            public bool AcceptsValidation { get; set; }

            public FakeValidation(bool acceptsValidation = false)
            {
                AcceptsValidation = acceptsValidation;
            }

            public bool ValidateEntryRequest(int id)
            {
                return AcceptsValidation;
            }
        }
    }
}
