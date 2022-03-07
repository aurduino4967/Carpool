namespace Carpool.Web.ViewModels
{
    public class OfferedRidePost
    {
        public DateTime Date { get; set; }
        public string Time { get; set; } = null!;
        public string FromPlace { get; set; } = null!;
        public string ToPlace { get; set; } = null!;
        public int Seats { get; set; }
        public string? Stops { get; set; }
        public double Price { get; set; }
    }
}
