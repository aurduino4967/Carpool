namespace Carpool.Web.ViewModels
{
    public class BookedRideView
    {
        public string OfferId { get; set; } = null!;
        public string BookedBy { get; set; } = null!;
        public int Seats { get; set; }
        public double Price { get; set; }
    }
}
