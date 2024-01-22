namespace HotelBooking.Domain.Abstractions
{
    /// <summary>
    /// Abstract class that contains Id property for entities.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        public Guid Id { get; set; }
    }
}
