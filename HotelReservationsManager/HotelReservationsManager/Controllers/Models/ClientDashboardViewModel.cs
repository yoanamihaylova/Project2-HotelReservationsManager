using HotelReservationsManager.Controllers.Models;
using System.Collections.Generic;
using Web.Models.Shared;

namespace HotelReservationsManager.Controllers.Models
{
    public class ClientDashboardViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<Client> Items { get; set; }

        public string firstNameFilter { get; set; }
        public string lasttNameFilter { get; set; }
    }
}