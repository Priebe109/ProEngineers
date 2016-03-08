namespace DoorControl.Defaults
{
    public class DefaultValidation : IUserValidation
    {
        public bool ValidateEntryRequest(int id)
        {
            return (id % 10 == 5);
        }
    }
}