namespace Booking_TrainTickets.Core.DTO
{
    public class CreateTrainDto
    {
        public int Id { get; set; }
        public string TrainName { get; set; }
        public int NumberofSeats { get; set; }
        public double price { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
    }
}
