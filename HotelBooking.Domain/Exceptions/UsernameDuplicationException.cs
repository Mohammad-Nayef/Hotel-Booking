namespace HotelBooking.Domain.Exceptions
{
    public class UsernameDuplicationException : Exception
    {
        public UsernameDuplicationException()
        {
        }

        public UsernameDuplicationException(string username)
            : base($"The user with username '{username}' already exists.")
        {
        }
    }
}
