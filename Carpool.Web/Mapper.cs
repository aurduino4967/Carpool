using Carpool.Core.ServiceModels;
using Carpool.Web.ViewModels;
namespace Carpool.Web.Mappers
{ 
    public class Mapper
    {

        public OfferedRideDTO Map(OfferedRide servicemodel)
        {
            OfferedRideDTO Ride = new()
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



        public ViewModels.BookedRideDTO Map(Core.ServiceModels.BookedRide servicemodel)
        {
            ViewModels.BookedRideDTO Ride = new()
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
