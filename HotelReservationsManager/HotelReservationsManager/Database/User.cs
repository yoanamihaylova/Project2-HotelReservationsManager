using System;
using HotelReservationsManager.Controllers.Models;

namespace HotelReservationsManager
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string EGN { get; set; }
        public bool isAdmin { get; set; }
        public bool isActive { get; set; }
        public string phoneNumber { get; set; }
        public DateTime dismisalDate { get; set; }
        public DateTime recruitmentDate { get; set; }

        public User() { }
        public User(UserDashboardRegulatedViewModel model)
        {
            this.id = model.id;
            this.username = model.username;
            this.password = model.password;
            this.isAdmin = model.isAdmin;
            this.EGN = model.EGN;
            this.isActive = model.isActive;
            this.firstName = model.firstName;
            this.secondName = model.secondName;
            this.thirdName = model.thirdName;
            this.EGN = model.EGN;
            this.phoneNumber = model.phoneNumber;
            this.recruitmentDate = model.recruitmentDate;
            this.dismisalDate = model.dismisalDate;
            this.isAdmin = model.isAdmin;
        }
        public User(User model)
        {
            this.id = model.id;
            this.username = model.username;
            this.password = model.password;
            this.isAdmin = model.isAdmin;
            this.EGN = model.EGN;
            this.isActive = model.isActive;
            this.firstName = model.firstName;
            this.secondName = model.secondName;
            this.thirdName = model.thirdName;
            this.EGN = model.EGN;
            this.phoneNumber = model.phoneNumber;
            this.recruitmentDate = model.recruitmentDate;
            this.dismisalDate = model.dismisalDate;
            this.isAdmin = model.isAdmin;
        }
    }
}