using HotelReservationsManager;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using Web.Models.Shared;

namespace HotelReservationsManager.Controllers.Models
{
    public class UserDashBoardViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<User> Items { get; set; }

        public string usernameFilter { get; set; }
        public string firstNameFilter { get; set; }
    }
}
