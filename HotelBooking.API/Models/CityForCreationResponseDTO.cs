﻿namespace HotelBooking.Api.Models
{
    public class CityForCreationResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string PostOffice { get; set; }
    }
}
