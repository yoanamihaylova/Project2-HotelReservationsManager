using System.ComponentModel.DataAnnotations;

namespace HotelReservationsManager
{
    public class RoomDashboardRegulatedViewModel
    {
        
        public int id { get; set; }
        
        public int capacity { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "type cannot be longer than 30 characters")]
        public string type { get; set; }

        public double priceAdult { get; set; }
        public double priceChild { get; set; }
        public int number { get; set; }
        public bool isFree { get; set; }

        public RoomDashboardRegulatedViewModel() { }
        public RoomDashboardRegulatedViewModel(Room other)
        {
            this.id = other.id;
            this.capacity = other.capacity;
            this.type = other.type;
            this.number = other.number;
            this.priceAdult = other.priceAdult;
            this.priceChild = other.priceChild;
            this.isFree = other.isFree;
        }
    }
}