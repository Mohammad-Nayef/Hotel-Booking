using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models.User;
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
            var user = _mapper.Map<UserTable>(newUser);

            user.Roles.ForEach(role =>
            {
                if (_dbContext.Entry(role).State == EntityState.Detached)
                {
                    _dbContext.Roles.Attach(role);
                }
            });

            var entityEntry = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }

        public Task<bool> ExistsAsync(Guid id) => 
            _dbContext.Users.AnyAsync(user => user.Id == id);

        public async Task<UserDTO> GetByUsernameIncludingRolesAsync(string username)
        {
            var neededUser = await _dbContext.Users
                .Include(user => user.Roles)
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Username == username);

            return neededUser == null? null : _mapper.Map<UserDTO>(neededUser);
        }

        public Task<bool> UsernameExistsAsync(string username) =>
            _dbContext.Users.AnyAsync(user => user.Username == username);
    }
}
