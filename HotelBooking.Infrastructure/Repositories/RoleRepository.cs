using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Repositories
{
    /// <inheritdoc cref="IRoleRepository"/>
    internal class RoleRepository : IRoleRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public RoleRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<RoleDTO> GetByNameAsync(string roleName)
        {
            var role = _dbContext.Roles
                .FirstOrDefault(role => role.Name == roleName);

            if (role == null)
                return await AddAsync(roleName);

            return _mapper.Map<RoleDTO>(role);
        }

        private async Task<RoleDTO> AddAsync(string roleName)
        {
            var newRole = new RoleTable
            {
                Name = roleName
            };

            await _dbContext.Roles.AddAsync(newRole);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<RoleDTO>(newRole);
        }
    }
}
