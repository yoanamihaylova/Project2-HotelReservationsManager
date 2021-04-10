using HotelReservationsManager.Controllers.Models;

namespace HotelReservationsManager
{
    public class Room
    {
        public int id { get; set; }
        public int capacity { get; set; }
        public string type { get; set; }
        public double priceAdult { get; set; }
        public double priceChild { get; set; }
        public int number { get; set; }
        public bool isFree { get; set; }

        public Room() { }
        public Room(Room other)
        {
            this.id = other.id;
            this.capacity = other.capacity;
            this.type = other.type;
            this.number = other.number;
            this.priceAdult = other.priceAdult;
            this.priceChild = other.priceChild;
            this.isFree = other.isFree;
        }
        public Room(RoomDashboardRegulatedViewModel other)
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