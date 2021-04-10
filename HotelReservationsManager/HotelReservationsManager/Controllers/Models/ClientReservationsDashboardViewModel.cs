using HotelReservationsManager.Controllers.Models;
using System.Collections.Generic;
using Web.Models.Shared;

namespace HotelReservationsManager.Controllers.Models
{
    public class ClientReservationsDashboardViewModel
    {
        public Client client { get; set; }
        public PagerViewModel Pager { get; set; }
        public List<Reservation> Items { get; set; }
    }
}