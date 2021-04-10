using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace HotelReservationsManager
{
    class DatabaseController
    {
        private HotelDb context;

        public DatabaseController() { }
        public DatabaseController(HotelDb context)
        {
            this.context = context;
        }

        public List <User> getAllUsers()
        {
            return context.users.ToList();
        }
        public List <Room> getAllRooms()
        {
            return context.rooms.ToList();
        }
        public List<Reservation> getAllReservations()
        {
            return context.reservations.ToList();
        }
        public List <Client> getAllClients()
        {
            return context.clients.ToList();
        }
        public List<ReservationClientLinker> getAllLinkers()
        {
            return context.linkers.ToList();
        }

        public int addUser(User u)
        {
            context.users.Add(u);
            context.SaveChanges();

            return u.id;
        }
        public int addRoom(Room r)
        {
            context.rooms.Add(r);
            context.SaveChanges();

            return r.id;
        }
        public int addLinker(ReservationClientLinker l)
        {
            context.linkers.Add(l);
            context.SaveChanges();

            return l.id;
        }
        public int addClient(Client c)
        {
            context.clients.Add(c);
            context.SaveChanges();

            return c.id;
        }
        public int addReservation(Reservation r)
        {
            context.reservations.Add(r);
            context.SaveChanges();

            return r.id;
        }

        public void removeUser(int id)
        {
            context.users.Remove(context.users.Find(id));
            context.SaveChanges();
        }
        public void removeRoom(int id)
        {
            context.rooms.Remove(context.rooms.Find(id));
            context.SaveChanges();
        }
        public void removeClient(int id)
        {
            context.clients.Remove(context.clients.Find(id));
            context.SaveChanges();
        }
        public void removeLinker(int id)
        {
            context.linkers.Remove(context.linkers.Find(id));
            context.SaveChanges();
        }

        public void updateRoom(Room r)
        {
            Room item = context.rooms.Find(r.id);
            context.Entry(item).CurrentValues.SetValues(r);

            context.SaveChanges();
        }
        public void updateUser(User u)
        {
            User item = context.users.Find(u.id);
            context.Entry(item).CurrentValues.SetValues(u);

            context.SaveChanges();
        }
        public void updateClient(Client c)
        {
            Client item = context.clients.Find(c.id);
            context.Entry(item).CurrentValues.SetValues(c);

            context.SaveChanges();
        }

        public void resetDb()
        {
            foreach (ReservationClientLinker l in context.linkers.ToList())
                context.linkers.Remove(l);
            foreach (Reservation r in context.reservations.ToList())
                context.reservations.Remove(r);
            foreach (User u in context.users.ToList())
                context.users.Remove(u);
            foreach (Room r in context.rooms.ToList())
                context.rooms.Remove(r);
            foreach (Client c in context.clients.ToList())
                context.clients.Remove(c);
            context.SaveChanges();
            

            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('users', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('rooms', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('reservations', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('clients', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('linkers', RESEED, 0)");
        }
    }
}