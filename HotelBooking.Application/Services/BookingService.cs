using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Abstractions.Utilities;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;
using HotelBooking.Domain.Models.User;

namespace HotelBooking.Application.Services
{
    /// <inheritdoc cref="IBookingService"/>
    internal class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IValidator<BookingDTO> _bookingValidator;
        private readonly IHotelDiscountRepository _hotelDiscountRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IHotelRepository _hotelRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            IValidator<BookingDTO> bookingValidator,
            IHotelDiscountRepository hotelDiscountRepository,
            IRoomRepository roomRepository,
            IEmailService emailService,
            IUserRepository userRepository,
            IHotelRepository hotelRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingValidator = bookingValidator;
            _hotelDiscountRepository = hotelDiscountRepository;
            _roomRepository = roomRepository;
            _emailService = emailService;
            _userRepository = userRepository;
            _hotelRepository = hotelRepository;
        }

        public async Task AddAsync(BookingDTO newBooking)
        {
            await _bookingValidator.ValidateAndThrowAsync(newBooking);

            newBooking.CreationDate = DateTime.UtcNow;
            var room = await _roomRepository.GetByIdAsync(newBooking.RoomId);
            var discount =
                _hotelDiscountRepository.GetHighestActiveDiscount(room.HotelId);
            AddPrice(newBooking, room, discount);

            await _bookingRepository.AddAsync(newBooking);
            await SendBookingDetailsEmailAsync(newBooking, room);
        }

        private void AddPrice(
            BookingDTO newBooking, RoomDTO room, DiscountDTO discount)
        {
            var discountPercentage = discount?.AmountPercent ?? 0;
            var originalPrice =
                ((newBooking.EndingDate - newBooking.StartingDate).Days + 1) * room.PricePerNight;
            newBooking.Price =
                originalPrice - originalPrice * (decimal)(discountPercentage / 100);
        }

        private async Task SendBookingDetailsEmailAsync(BookingDTO booking, RoomDTO room)
        {
            var user = await _userRepository.GetByIdAsync(booking.UserId);
            var hotel = await _hotelRepository.GetByIdAsync(room.HotelId);
            string emailBody = GenerateEmailBody(booking, room, user, hotel);
            var email = new EmailDTO
            {
                ToName = $"{user.FirstName} {user.LastName}",
                ToEmail = user.Email,
                Subject = "Booked room",
                Body = emailBody

            };

            await _emailService.SendAsync(email);
        }

        private static string GenerateEmailBody(
            BookingDTO booking, RoomDTO room, UserDTO user, HotelDTO hotel)
        {
            var googleMapsLink =
                $"https://www.google.com/maps/search/?api=1&query={hotel.Geolocation}";
            var emailBody =
                $"Dear {user.FirstName},\n" +
                $"We are delighted to confirm your booking made through our website. " +
                $"Your booking was created on {booking.CreationDate}, with your stay scheduled " +
                $"to start on {booking.StartingDate} and end on {booking.EndingDate}.\n" +
                $"The total price for your booking is {booking.Price:C}." +
                $"You have booked room number {room.Number} which can accommodate " +
                $"{room.AdultsCapacity} adults and {room.ChildrenCapacity} children.\n" +
                $"Your stay will be at {hotel.Name}, a {hotel.StarRating}-star hotel. " +
                $"You can view the exact location of the hotel on the map here: {googleMapsLink}" +
                $" \nWe hope you have a pleasant stay. \nIf you have any questions or need " +
                $"further assistance, feel free to contact us.\n\n" +
                $"Best Regards,\nYour Booking Website Team";

            return emailBody;
        }
    }
}
