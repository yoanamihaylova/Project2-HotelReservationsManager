
namespace HotelReservationsManager
{
    public class ReservationClientLinker
    {
        public int id { get; set; }

        public  virtual Client client { get; set; }
        public virtual Reservation reservation { get; set; }

        public ReservationClientLinker() { }
        public ReservationClientLinker(ReservationClientLinker other)
        {
            this.id = other.id;
            this.client = new Client(other.client);
            this.reservation = new Reservation(other.reservation);
        }
    }
}