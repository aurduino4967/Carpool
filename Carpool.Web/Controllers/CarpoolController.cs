using Microsoft.AspNetCore.Mvc;
using Carpool.Core.Services;
using Carpool.Core.ServiceModels;
using Carpool.Web.ViewModels;
using Carpool.Web.Mappers;
using Carpool.Core.Interfaces;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Carpool.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarpoolController : ControllerBase
    {

        private readonly CarpoolUserService user=new CarpoolUserService(new CarpoolDBService());
        private readonly Mapper mapper = new();
        //to sign in 
    
        [HttpPost("Signup")]
        public IActionResult Signup([FromBody] UserDTO Newuser )
        {
            return Ok(user.Signup(mapper.Map(Newuser)));
        }

        //to login to the portal
        [HttpGet("login/{id,password}")]
        public IActionResult Login(string id,string password)
        {
            UserDTO ExistingUser = new()
            {
                Uname = id,
                Password = password,
            };
             return Ok(user.Login(mapper.Map(ExistingUser)));
        }
    

        // POST api/<tpoolController>
        [HttpPost("OfferRide")]
        public IActionResult OfferRide([FromBody] OfferedRidePostDTO Ride)
        {
            return Ok(user.OfferRide(mapper.Map(Ride)));

        }


        //to book a ride
        [HttpPost("BookRide")]
        public IActionResult BookRide([FromBody] BookRidePostDTO Ride)
        {
            return Ok(user.BookRide(mapper.Map(Ride)));
        }


        //to know the available rides 
        [HttpGet("AvailableRides/{date,time,fromplace,toplace,seats}")]
        public IEnumerable<OfferedRideDTO> AvailableRides(DateTime date, string time, string fromplace, string toplace, int seats)
        {
            List<OfferedRideDTO> res = new();
            OfferedRide SearchRide = new()
            {
                Date = date,
                Time = time,
                FromPlace = fromplace,
                ToPlace = toplace,
                Seats = seats,

            };
            foreach (OfferedRide Ride in user.AvailableRides(SearchRide))
            {
                res.Add(mapper.Map(Ride));
            }
            return res;
        }


        // GET: api/<tpoolController>
        //to get the history of rides 
        [HttpGet("History")]
        public IEnumerable<OfferedRideDTO> History()
        {
            List<OfferedRideDTO> res = new();
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
        [HttpGet("IsAuthorized")]
        public IActionResult IsAuthorized()
        {
            return Ok(user.GetEmail());
        }
       
    }
}
