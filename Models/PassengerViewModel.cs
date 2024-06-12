namespace TrainTicketBookingUI.Models
{
    public class PassengerViewModel
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
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string ConfidentialComment { get; set; } = "Normal";
    }
}
