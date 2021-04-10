using HotelReservationsManager.Controllers.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HotelReservationsManager
{
    public class Reservation
    {
        public int id { get; set; }
        public virtual Room room { get; set; }
        public virtual User user { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public bool breakfast { get; set; }
        public bool allInclusive { get; set; }
        public double cost { get; set; }

        public virtual ICollection<ReservationClientLinker> clients { get; set; }

        public Reservation() { }
        public Reservation(Reservation other)
        {
            this.id = other.id;
            this.room = new Room(other.room);
            this.user = new User(other.user);
            this.dateStart = other.dateStart;
            this.dateEnd = other.dateEnd;
            this.breakfast = other.breakfast;
            this.allInclusive = other.allInclusive;
            this.cost = other.cost;
            this.clients = other.clients;
        }
    }
}