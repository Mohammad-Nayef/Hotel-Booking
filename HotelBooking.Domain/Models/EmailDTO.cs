namespace HotelBooking.Domain.Models
{
    /// <summary>
    /// Details about an email to send.
    /// </summary>
    public class EmailDTO
    {
        /// <summary>
        /// Email to send a message to.
        /// </summary>
        public string ToEmail { get; set; }
        /// <summary>
        /// Name of the person to send a message to.
        /// </summary>
        public string ToName { get; set; }
        /// <summary>
        /// Subject of the message.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Body of the message.
        /// </summary>
        public string Body { get; set; }
    }
}
