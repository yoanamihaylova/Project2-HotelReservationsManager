using HotelReservationsManager.Controllers.Models;
using System.Collections.Generic;
using Web.Models.Shared;

namespace HotelReservationsManager.Controllers.Models
{
    public class RoomDashboardViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<Room> Items { get; set; }

        public int? capMinFilter { get; set; }
        public int? capMaxFilter { get; set; }
        public bool isFreeFilter { get; set; }
        public string typeFilter { get; set; }
    }
}