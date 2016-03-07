namespace DoorControl
{
    namespace Defaults
    {
        public class UserValidation : IUserValidation
        {
            public bool ValidateEntryRequest(int id)
            {
                return (id % 10 == 5);
            }
        }
    }
}