using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Infrastructure.Repositories
{
    /// <inheritdoc cref="IDiscountRepository"/>
    internal class DiscountRepository : IDiscountRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountRepository> _logger;

        public DiscountRepository(
            HotelsBookingDbContext dbContext, IMapper mapper, ILogger<DiscountRepository> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> AddAsync(DiscountDTO newDiscount)
        {
            var entityEntry = await _dbContext.Discounts.AddAsync(
                _mapper.Map<DiscountTable>(newDiscount));
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Created discount with Id: {id}", entityEntry.Entity.Id);
            return entityEntry.Entity.Id;
        }
    }
}
