using System.ComponentModel.DataAnnotations;
using System;

namespace HotelReservationsManager
{
    public class ReservationDashboardRegulatedViewModel
    {
        [Required]
        public virtual int? roomNumber { get; set; }

        [Required]
        public DateTime dateStart { get; set; }
        
        public DateTime dateEnd { get; set; }
        public bool breakfast { get; set; }
        public bool allInclusive { get; set; }

        public double cost { get; set; }

        public int clientsCnt { get; set; }
        public ClientDashboardRegulatedViewModel[] clients { get; set; }

        public string errorMessage { get; set; }
    }
}