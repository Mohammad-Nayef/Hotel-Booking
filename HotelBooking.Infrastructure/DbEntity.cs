﻿namespace HotelBooking.Infrastructure
{
    /// <summary>
    /// Abstract class that contains Id property for database entities.
    /// </summary>
    internal class DbEntity
    {
        /// <summary>
        /// Id of the entitiy.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
