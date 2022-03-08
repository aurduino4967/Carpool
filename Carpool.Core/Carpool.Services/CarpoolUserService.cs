using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.Core.ServiceModels;
using Carpool.Core.Interfaces;

namespace Carpool.Core.Services
{
    public  class CarpoolUserService: CarpoolUserServiceImp
    {
        private string Email = String.Empty;
        private CarpoolDBService DBObject;
        public CarpoolUserService()
        {
           DBObject = new CarpoolDBService();
        }

        public bool IsAuthorized()
        {
            return !string.IsNullOrEmpty(Email);
        }
     
        public string Signup(String email, String password)
        {
            User Newuser = new()
            {
                Uname = email,
                Password = password
            };
            return DBObject.CreateNewUser(Newuser);
            
        }

        public List<OfferedRide> AvailableRides(DateTime date, string time, string fromplace, string toplace, int seats)
        {
            List<OfferedRide> res = new();
            bool flag;
            toplace=toplace.ToLower();
            string[] stops;
            try
            {

                foreach (OfferedRide Ride in DBObject.GetOfferedRides())
                {
                    flag = false;
                    
                    if (Ride.Date == date && Ride.Time.TrimEnd() == time && Ride.FromPlace.TrimEnd() == fromplace.ToLower() && Ride.Seats >= seats)
                    {
                        if (Ride.Stops != null && Ride.Stops.Contains(','))
                        {
                            stops = Ride.Stops.Split(',');


                            foreach (string stop in stops)
                            {
                                if (stop.TrimEnd().Equals(toplace) || Ride.ToPlace.Equals(toplace))
                                {
                                    flag = true;
                                }

                            }
                        }
                        else if ((Ride.Stops != null && Ride.Stops.TrimEnd() == toplace) || Ride.ToPlace == toplace)
                            flag = true;

                        
                    }
                    if (flag)
                        res.Add(Ride);

                }
                return res;
            }
            catch
            { return new List<OfferedRide>();}
        }

       


        public string Login(string email, string password)
        {
            try
            {
                User? CurrentUser = DBObject.GetUser(email, password);
                if (CurrentUser != null)
                {
                    Email = CurrentUser.Uname;
                    return "done successfully";
                }

                else
                {
                    return "invalid login credentials";
                }
            }
            catch (Exception ex)    
            {
                return ex.Message;
            }

        }

        public string OfferRide(DateTime date, string time, string from, string to, string stops,float price, int seats)
        {
            if (IsAuthorized())
            {

                OfferedRide newride = new();
                {
                    newride.OfferedBy = Email;
                    newride.OfferId = newride.OfferedBy + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss");
                    newride.Date = date;
                    newride.Time = time;
                    newride.FromPlace = from.ToLower();
                    newride.ToPlace = to.ToLower();
                    newride.Stops = stops;
                    newride.Price = price;
                    newride.Seats = seats;
                }
                return DBObject.CreateNewOfferedRide(newride);
            }
            else 
            { return "do login"; }
            

        }


        public string BookRide(string offerid, int seats)
        {
            if (IsAuthorized())
            {
                try
                {
                    OfferedRide? Ride = DBObject.GetOfferedRideById(offerid);
                    if (Ride != null)
                    {
                        BookedRide booking = new BookedRide()
                        {
                            OfferId = offerid,
                            Seats = seats,
                            BookedBy = Email,
                            Price = Ride.Price * seats
                        };

                        Ride.Seats -= seats;
                        DBObject.CreateNewBookedRide(booking);
                        DBObject.UpdateOfferedRide(Ride);
                        return "Ride Booked Successfully";
                    }
                    else
                    { return "invalid offerid"; }

                }
                catch (Exception e)
                { return e.Message; }
            }
            else
            { return "Do LogIn "; }

        }



        public List<OfferedRide> OfferedRides()
        {

            List<OfferedRide> res = new List<OfferedRide>();

            foreach (OfferedRide Ride in DBObject.GetOfferedRides())
            {
                if (Ride.OfferedBy.TrimEnd() == Email)
                {

                    res.Add(Ride);
                }

            }
            return res;

        }


        public List<OfferedRide> BookedRides()
        {

            List<OfferedRide> res = new();
            List<BookedRide> Bookings=new List<BookedRide>();   

            foreach (BookedRide booked in DBObject.GetBookedRides())
            {
                if (booked.BookedBy.TrimEnd() == Email)
                {
                    Bookings.Add(booked);
                }
            }
            foreach(BookedRide Booked in Bookings)
                    res.Add(item: DBObject.GetOfferedRideById(Booked.OfferId));
            return res;

        }
    }
}
