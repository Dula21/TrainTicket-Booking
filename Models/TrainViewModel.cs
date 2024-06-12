using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainTicketBookingUI.Models
{
    public class TrainViewModel
    {
        public int Id { get; set; }
        public string TrainName { get; set; }
        
        public int NumberofSeats { get; set; }
        public double price { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string ConfidentialComment { get; set; } = "Normal";
    }
}
