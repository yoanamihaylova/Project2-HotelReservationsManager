using HotelReservationsManager.Controllers.Models;

namespace HotelReservationsManager
{
    public class Client
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public bool isAdult { get; set; }

        public Client() { }
        public Client(Client other)
        {
            this.id = other.id;
            this.firstName = other.firstName;
            this.lastName = other.lastName;
            this.phoneNumber = other.phoneNumber;
            this.email = other.email;
            this.isAdult = other.isAdult;
        }
        public Client(ClientDashboardRegulatedViewModel other)
        {
            this.id = other.id;
            this.firstName = other.firstName;
            this.lastName = other.lastName;
            this.phoneNumber = other.phoneNumber;
            this.email = other.email;
            this.isAdult = other.isAdult;
        }
    }
}