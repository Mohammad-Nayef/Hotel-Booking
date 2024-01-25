using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models.User;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Infrastructure.Repositories
{
    /// <inheritdoc cref="IUserRepository"/>
    internal class UserRepository : IUserRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            HotelsBookingDbContext dbContext, IMapper mapper, ILogger<UserRepository> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            return _mapper.Map<UserDTO>(
                await _dbContext.Users
                    .FindAsync(id));
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

            _logger.LogInformation("Created new user with Id: {id}", entityEntry.Entity.Id);
            return entityEntry.Entity.Id;
        }

        public Task<bool> ExistsAsync(Guid id) =>
            _dbContext.Users.AnyAsync(user => user.Id == id);

        public async Task<UserDTO> GetByUsernameIncludingRolesAsync(string username)
        {
            var neededUser = await _dbContext.Users
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(user => user.Username == username);

            return neededUser == null ? null : _mapper.Map<UserDTO>(neededUser);
        }

        public Task<bool> UsernameExistsAsync(string username) =>
            _dbContext.Users.AnyAsync(user => user.Username == username);
    }
}
