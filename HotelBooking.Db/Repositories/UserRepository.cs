using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Db.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly HotelsBookingDbContext _dbContext;

        public UserRepository(HotelsBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ThrowExceptionIfIdOrUsernameExists(UserDTO userToFind)
        {
            bool usernameExists = false;

            _dbContext.Users.ForEachAsync(user =>
            {
                if (user.Id == userToFind.Id)
                    throw new IdDuplicationException(user.Id);

                usernameExists = user.Username == userToFind.Username;
            });

            if (usernameExists)
                throw new UsernameDuplicationException(userToFind.Username);
        }
    }
}
