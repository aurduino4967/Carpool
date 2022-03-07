using Microsoft.AspNetCore.Mvc;
using Carpool.Services;
using Carpool.Models;
using Carpool.Web.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Carpool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarpoolController : ControllerBase
    {

        private readonly CarpoolService user = new();
        private readonly Mapper mapper = new();
        //to sign in 
        [HttpPost("Signup")]
        public IActionResult Signup([FromBody] UserPost Newuser )
        {
            return Ok(user.Signup(Newuser.Uname, Newuser.Password));
        }

        //to login to the portal
        [HttpGet("login/{id,password}")]
        public IActionResult Login(string id, string password)
        {
             return Ok(user.Login(id,password));
        }
    

        // POST api/<tpoolController>
        [HttpPost("OfferRide")]
        public IActionResult OfferRide([FromBody] OfferedRidePost Ride)
        {
            return Ok(user.OfferRide(Ride.Date, Ride.Time, Ride.FromPlace, Ride.ToPlace, Ride.Stops, (float)Ride.Price, Ride.Seats));

        }


        //to book a ride
        [HttpPost("BookRide")]
        public IActionResult BookRide([FromBody] BookedRideView Ride)
        {
            return Ok(user.BookRide(Ride.OfferId, Ride.Seats));
        }


        //to know the available rides 
        [HttpGet("AvailableRides/{date,time,fromplace,toplace,seats}")]
        public IEnumerable<OfferedRideView> AvailableRides(DateTime date, string time, string fromplace, string toplace, int seats)
        {
            List<OfferedRideView> res = new();
            foreach (OfferedRide Ride in user.AvailableRides(date, time, fromplace, toplace, seats))
            {
                res.Add(mapper.Map(Ride));

            }
            return res;
        }


        // GET: api/<tpoolController>
        //to get the history of rides 
        [HttpGet("History")]
        public IEnumerable<OfferedRideView> History()
        {
            List<OfferedRideView> res = new();
            foreach (OfferedRide Ride in user.OfferedRides())
            {
                res.Add(mapper.Map(Ride));

            }
            foreach (OfferedRide Ride in user.BookedRides())
            {
                res.Add(mapper.Map(Ride));

            }
            return res;
        }
  
       
    }
}
