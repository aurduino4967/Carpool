using Carpool.Core.ServiceModels;
using Carpool.Web.ViewModels;
namespace Carpool.Web.Mappers
{ 
    public class DTOMapper
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



        public BookedRideDTO Map(BookedRide servicemodel)
        {
            BookedRideDTO Ride = new()
            {
                Seats = servicemodel.Seats,
                Price = servicemodel.Price,
                OfferId = servicemodel.OfferId,
                BookedBy = servicemodel.BookedBy,
            };
            return Ride;
        }

        public BookedRide Map(BookRidePostDTO viewmodel)
        {
            BookedRide servicemodel = new()
            {
                Seats = viewmodel.Seats,
                OfferId = viewmodel.OfferId,

            };
            return servicemodel;
        }
        public OfferedRide Map(OfferedRidePostDTO viewmodel)
        {
            OfferedRide servicemodel = new()
            {
                Time = viewmodel.Time,
                Date = viewmodel.Date,
                Price = viewmodel.Price,
                Seats = viewmodel.Seats,
                FromPlace = viewmodel.FromPlace,
                Stops = viewmodel.Stops,
                ToPlace = viewmodel.ToPlace,
            };
            return servicemodel;
        }
        public User Map(UserDTO viewmodel)
        {
            User servicemodel = new()
            {
                Uname = viewmodel.Uname,
                Password =viewmodel.Password,
            };
            return servicemodel;

        }
    }
    
}
