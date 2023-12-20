namespace HotelBooking.Application.Validations
{
    internal class PaginationValidator
    {
        public static bool Validate(int pageNumber, int pageSize)
        {
            var negativePageNumber = pageNumber <= 0;
            var negativePageSize = pageSize <= 0;

            if (negativePageNumber && negativePageSize)
                throw new Exception($"'{pageNumber}' and '{pageSize}' must be greater than 0.");

            if (negativePageNumber)
                throw new Exception($"'{pageNumber}' must be greater than 0.");

            if (negativePageSize)
                throw new Exception($"'{pageSize}' must be greater than 0.");

            return true;
        }
    }
}
