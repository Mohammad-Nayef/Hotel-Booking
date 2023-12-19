namespace HotelBooking.Domain.Exceptions
{
    public class IdDuplicationException : Exception
    {
        public IdDuplicationException()
        {
        }

        public IdDuplicationException(Guid id)
            : base($"the entity with Id '{id}' already exists.")
        {
        }
    }
}