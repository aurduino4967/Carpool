using Carpool.WebApi;
using Carpool.Models;
using Carpool.Web.ViewModels;
namespace Carpool.WebApi
{
    public class Mapper
    {
        public OfferedRideView Map(OfferedRide servicemodel)
        {
            OfferedRideView Ride = new()
            {
                Date = servicemodel.Date,
                FromPlace = servicemodel.FromPlace,
                OfferId = servicemodel.OfferId,
                Seats = servicemodel.Seats,
                OfferedBy = servicemodel.OfferedBy,
                Price = servicemodel.Price,
                ToPlace = servicemodel.ToPlace,
                Stops = servicemodel.Stops,
                Time = servicemodel.Time

            };
            return Ride;
        }
        public BookedRideView Map( BookedRide servicemodel)
        {
            BookedRideView Ride = new()
            {
                Seats = servicemodel.Seats,
                Price = servicemodel.Price,
                OfferId = servicemodel.OfferId,
                BookedBy = servicemodel.BookedBy,
            };
            return Ride;
        }
    }
    
}
