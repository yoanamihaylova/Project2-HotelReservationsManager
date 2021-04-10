using HotelReservationsManager.Controllers.Models;
using System.Collections.Generic;
using Web.Models.Shared;

namespace HotelReservationsManager.Controllers.Models
{
    public class ReservationDashboardViewModel
    {
        public PagerViewModel Pager { get; set; }
        public ICollection<Reservation> Items { get; set; }
    }
}