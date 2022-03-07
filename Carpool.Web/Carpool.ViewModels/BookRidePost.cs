namespace Carpool.Web.ViewModels
{
    public class BookRidePost
    {
        public string OfferId { get; set; } = null!;
        public int Seats { get; set; }
        public double Price { get; set; }
    }
}
