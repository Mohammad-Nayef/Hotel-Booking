using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Db.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(UserDTO newUser)
        {
            var entityEntry = await _dbContext.Users.AddAsync(_mapper.Map<UserTable>(newUser));
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }

        public async Task<UserDTO> GetByUsernameIncludingRolesAsync(string username)
        {
            var neededUser = await _dbContext.Users
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(user => user.Username == username);

            return neededUser == null? null : _mapper.Map<UserDTO>(neededUser);
        }

        public Task<bool> UsernameExistsAsync(string username) =>
            _dbContext.Users.AnyAsync(user => user.Username == username);
    }
}
