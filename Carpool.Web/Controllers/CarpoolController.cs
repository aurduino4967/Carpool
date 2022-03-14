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
        private readonly CarpoolUserServiceImp User =  CarpoolUserService.Instance;
        private readonly DTOMapper Mapper=new DTOMapper();
        
        //to sign in 
        [HttpPost("Signup")]
        public IActionResult Signup([FromBody] UserDTO newUser )
        {
            return Ok(User.Signup(Mapper.Map(newUser)));
        }

        //to login to the portal
        [HttpGet("login/{id,password}")]
        public IActionResult Login(string id,string password)
        {
            UserDTO existingUser = new()
            {
                Uname = id,
                Password = password,
            };
             return Ok(User.Login(Mapper.Map(existingUser)));
        }
    
        // POST api/<tpoolController>
        [HttpPost("OfferRide")]
        public IActionResult OfferRide([FromBody] OfferedRidePostDTO ride)
        {
            return Ok(User.OfferRide(Mapper.Map(ride)));

        }

        //to book a ride
        [HttpPost("BookRide")]
        public IActionResult BookRide([FromBody] BookRidePostDTO ride)
        {
            return Ok(User.BookRide(Mapper.Map(ride)));
        }

        //to know the available rides 
        [HttpGet("AvailableRides/{date,time,fromplace,toplace,seats}")]
        public IEnumerable<OfferedRideDTO> AvailableRides(DateTime date, string time, string fromplace, string toplace, int seats)
        {
            List<OfferedRideDTO> availableRidesList = new();
            OfferedRide searchRide = new()
            {
                Date = date,
                Time = time,
                FromPlace = fromplace,
                ToPlace = toplace,
                Seats = seats,

            };
            foreach (OfferedRide ride in User.AvailableRides(searchRide))
            {
                availableRidesList.Add(Mapper.Map(ride));
            }
            return availableRidesList;
        }

        // GET: api/<tpoolController>
        [HttpGet("History")]
        public IEnumerable<OfferedRideDTO> History()
        {
            List<OfferedRideDTO> res = new();
            foreach (OfferedRide ride in User.OfferedRides())
            {
                res.Add(Mapper.Map(ride));
            }
            foreach (OfferedRide ride in User.BookedRides())
            {
                res.Add(Mapper.Map(ride));
            }
            return res;
        }

    }
}
