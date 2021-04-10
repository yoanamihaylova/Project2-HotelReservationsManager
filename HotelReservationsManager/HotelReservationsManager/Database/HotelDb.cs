using Microsoft.EntityFrameworkCore;
using System;

namespace HotelReservationsManager
{
    public class HotelDb : DbContext
    {
        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<Room> rooms { get; set; }
        public virtual DbSet<Client> clients { get; set; }
        public virtual DbSet<Reservation> reservations { get; set; }
        public virtual DbSet<ReservationClientLinker> linkers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = (localdb)\.; Database = HotelDb1;");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}