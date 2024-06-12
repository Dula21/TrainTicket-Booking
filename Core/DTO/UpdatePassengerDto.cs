namespace Booking_TrainTickets.Core.DTO
{
    public class UpdatePassengerDto
    {
        public long Id { get; set; }
        public string PassengerName { get; set; }
        public int PassengerID { get; set; }
        public double total { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TrainName { get; set; }

        public string Class { get; set; }

        public DateTime DateTime { get; set; }
        

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
