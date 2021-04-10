using System.ComponentModel.DataAnnotations;

namespace HotelReservationsManager
{
    public class ClientDashboardRegulatedViewModel
    {
        public int id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "first name cannot be longer than 30 characters")]
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public bool isAdult { get; set; }

        public ClientDashboardRegulatedViewModel() { }
        public ClientDashboardRegulatedViewModel(Client other)
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