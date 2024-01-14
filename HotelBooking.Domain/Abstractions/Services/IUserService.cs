﻿using HotelBooking.Domain.Models.User;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IUserService
    {
        Task<Guid> AddAsync(UserDTO user);
        Task<string> AuthenticateAsync(UserLoginDTO userLogin);
        Task<bool> ExistsAsync(Guid userId);
    }
}
