namespace HotelBooking.Domain.Abstractions
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
