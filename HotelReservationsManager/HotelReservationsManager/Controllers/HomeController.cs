using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HotelReservationsManager.Controllers.Models;
using Web.Models.Shared;

namespace HotelReservationsManager.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseController dc;
        private const int PageSize = 10;
        private readonly ILogger<HomeController> _logger;

        public static User loggedUser = null;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            this.dc = new DatabaseController(new HotelDb());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard(User u)
        {
            if (loggedUser == null) return NotFound();
            return View();
        }

        public IActionResult tryLogin(User u)
        {
            User match = dc.getAllUsers().FirstOrDefault(x => x.username == u.username);

            if (match == null) return RedirectToAction(nameof(Index));
            if(match.isActive==false) return RedirectToAction(nameof(Index));
            if (match.password != u.password) return RedirectToAction(nameof(Index));

            loggedUser = match;
            return RedirectToAction(nameof(Dashboard));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ManageUsers(UserDashBoardViewModel model)
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            if (model.usernameFilter == "") model.usernameFilter = null;
            if (model.firstNameFilter == "") model.firstNameFilter = null;

            List<User> items = dc.getAllUsers().Skip((model.Pager.CurrentPage - 1) * PageSize)
            .Where(u => model.usernameFilter == null || model.usernameFilter == u.username)
            .Where(u => model.firstNameFilter == null || model.firstNameFilter == u.firstName)
            .Take(PageSize).Select(b => new User(new UserDashboardRegulatedViewModel(b))).ToList();
            int cnt = items.Count;

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(cnt / (double)PageSize);

            return View(model);
        }
        public IActionResult ManageRooms(RoomDashboardViewModel model)
        {
            if (loggedUser is null) return NotFound();

            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            int? capMinFilter = model.capMinFilter, capMaxFilter = model.capMaxFilter;
            if (model.typeFilter == "") model.typeFilter = null;
            if (model.capMinFilter == null) capMinFilter = -1;
            if (model.capMaxFilter == null) capMaxFilter = int.MaxValue;

            int cnt = dc.getAllRooms().Count;

            List<Room> items = dc.getAllRooms().Skip((model.Pager.CurrentPage - 1) * PageSize)
            .Where(r => capMinFilter <= r.capacity && r.capacity <= capMaxFilter)
            .Where(r => ((r.isFree & model.isFreeFilter) == model.isFreeFilter))
            .Where(r => model.typeFilter == null || model.typeFilter == r.type)
            .Take(PageSize).Select(r => new Room(r)).ToList();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(cnt / (double)PageSize);

            return View(model);
        }
        public IActionResult ManageClients(ClientDashboardViewModel model)
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            if (model.firstNameFilter == "") model.firstNameFilter = null;
            if (model.lasttNameFilter == "") model.lasttNameFilter = null;

            int cnt = dc.getAllClients().Count;

            List<Client> items = dc.getAllClients().Skip((model.Pager.CurrentPage - 1) * PageSize)
            .Where(c => model.firstNameFilter == null || model.firstNameFilter == c.firstName)
            .Where(c => model.lasttNameFilter == null || model.lasttNameFilter == c.lastName)
            .Take(PageSize).Select(c => new Client(c)).ToList();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(cnt / (double)PageSize);

            return View(model);
        }
        public IActionResult SeeReservations(ReservationDashboardViewModel model)
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            int cnt = dc.getAllReservations().Count;

            List<Reservation> items = dc.getAllReservations().Skip((model.Pager.CurrentPage - 1) * PageSize)
            .Take(PageSize).Select(r => new Reservation(r)).ToList();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(cnt / (double)PageSize);

            return View(model);
        }

        [HttpGet]
        public IActionResult GetClientReservations(ClientReservationsDashboardViewModel model, int clientId)
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            model.Pager ??= new PagerViewModel();
            model.client = dc.getAllClients().FirstOrDefault(c => c.id == clientId);
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<Reservation> items = dc.getAllReservations().Skip((model.Pager.CurrentPage - 1) * PageSize)
            .Where(r => r.clients.Any(c => c.client != null && c.client.id == clientId) == true)
            .Take(PageSize).Select(r => new Reservation(r)).ToList();
            int cnt = items.Count;

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(cnt / (double)PageSize);

            return View(model);
        }

        public IActionResult MakeReservation1()
        {
            if (loggedUser == null) return NotFound();

            ReservationDashboardRegulatedViewModel model = new ReservationDashboardRegulatedViewModel();

            model.clients = new ClientDashboardRegulatedViewModel[model.clientsCnt];
            for (int i = 0; i < model.clients.Length; i++) model.clients[i] = new ClientDashboardRegulatedViewModel();

            return View(model);
        }
        public IActionResult CreateUser()
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            UserDashboardRegulatedViewModel model = new UserDashboardRegulatedViewModel();
            return View(model);
        }
        public IActionResult CreateRoom()
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            RoomDashboardRegulatedViewModel model = new RoomDashboardRegulatedViewModel();
            return View(model);
        }
        public IActionResult CreateClient()
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            ClientDashboardRegulatedViewModel model = new ClientDashboardRegulatedViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(UserDashboardRegulatedViewModel model)
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            if (ModelState.IsValid)
            {
                User u = new User(model);
                dc.addUser(u);

                return RedirectToAction(nameof(ManageUsers));
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRoom(RoomDashboardRegulatedViewModel model)
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            if (ModelState.IsValid)
            {
                Room r = new Room(model);
                dc.addRoom(r);

                return RedirectToAction(nameof(ManageRooms));
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateClient(ClientDashboardRegulatedViewModel model)
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            if (ModelState.IsValid)
            {
                Client r = new Client(model);
                dc.addClient(r);

                return RedirectToAction(nameof(ManageClients));
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeReservation2(ReservationDashboardRegulatedViewModel model)
        {
            model.errorMessage = "";
            if (loggedUser == null) return NotFound();

            if (ModelState.IsValid == false) return View(model);
            if (model.roomNumber == null) return View(model);

            Room r = dc.getAllRooms().FirstOrDefault(r => r.number == model.roomNumber);

            if (r is null)
            {
                model.errorMessage = "This room does not exist!";
                return View(model);
            }
            if (r.isFree == false)
            {
                model.errorMessage = "This room is occupied!";
                return View(model);
            }

            r.isFree = false;
            dc.updateRoom(r);

            Client[] clients = new Client[model.clientsCnt];
            for (int i = 0; i < model.clientsCnt; i++)
            {
                clients[i] = new Client(model.clients[i]);
                clients[i].id = 0;
            }

            Reservation res = new Reservation()
            {
                id = 0,
                user = dc.getAllUsers().FirstOrDefault(u => u.id == HomeController.loggedUser.id),
                room = r,
                dateStart = model.dateStart,
                dateEnd = model.dateEnd,
                allInclusive = model.allInclusive,
                breakfast = model.breakfast,
            };
            res.cost = (model.dateEnd - model.dateStart).TotalDays * (clients.Count(c => c.isAdult == true) * r.priceAdult + clients.Count(c => c.isAdult == false) * r.priceChild
                       + ((res.breakfast == true) ? 7 : 0) + ((res.allInclusive == true) ? 8 : 0));

            int id = dc.addReservation(res);
            res = dc.getAllReservations().FirstOrDefault(r => r.id == id);

            foreach (Client c in clients)
            {
                Client client = dc.getAllClients().FirstOrDefault(x => x.firstName == c.firstName && x.lastName == c.lastName);
                if (client == null)
                {
                    id = dc.addClient(c);
                    client = dc.getAllClients().FirstOrDefault(x => x.id == id);
                }

                
                id = dc.addLinker(new ReservationClientLinker() { client = client, reservation = res });
            }

            return RedirectToAction(nameof(Dashboard));
        }

        public IActionResult deleteUser(int id)
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            dc.removeUser(id);
            return RedirectToAction(nameof(ManageUsers));
        }
        public IActionResult DeleteRoom(int id)
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            dc.removeRoom(id);
            return Redirect("/Home/ManageRooms");
        }
        public IActionResult DeleteClient(int id)
        {
            if (loggedUser == null) return NotFound();
            if (loggedUser.isAdmin == false) return NotFound();

            List<ReservationClientLinker> linkers = dc.getAllLinkers();
            linkers = linkers.Where(l => l.client.id == id).ToList();

            foreach (ReservationClientLinker l in linkers) dc.removeLinker(l.id);
            dc.removeClient(id);

            return Redirect("/Home/ManageClients");
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            User u = dc.getAllUsers().FirstOrDefault(u => u.id == id);
            UserDashboardRegulatedViewModel model = new UserDashboardRegulatedViewModel(u);

            return View(model);
        }
        [HttpGet]
        public IActionResult EditRoom(int id)
        {
            Room r = dc.getAllRooms().FirstOrDefault(r => r.id == id);
            RoomDashboardRegulatedViewModel model = new RoomDashboardRegulatedViewModel(r);

            return View(model);
        }
        [HttpGet]
        public IActionResult EditClient(int id)
        {
            Client c = dc.getAllClients().FirstOrDefault(c => c.id == id);
            ClientDashboardRegulatedViewModel model = new ClientDashboardRegulatedViewModel(c);

            return View(model);
        }

        public IActionResult EditUser(UserDashboardRegulatedViewModel model)
        {
            if (ModelState.IsValid)
            {
                User u = new User(model);
                dc.updateUser(u);

                return Redirect("/Home/ManageUsers");
            }

            return View(model);
        }
        public IActionResult EditRoom(RoomDashboardRegulatedViewModel model)
        {
            if (ModelState.IsValid)
            {
                Room r = new Room(model);
                dc.updateRoom(r);

                return Redirect("/Home/ManageRooms");
            }

            return View(model);
        }
        public IActionResult EditClient(ClientDashboardRegulatedViewModel model)
        {
            if (ModelState.IsValid)
            {
                Client c = new Client(model);
                dc.updateClient(c);

                return Redirect("/Home/ManageClients");
            }

            return View(model);
        }

        public IActionResult logout()
        {
            HomeController.loggedUser = null;
            return Redirect("/Home");
        }
    }
}
