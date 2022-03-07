using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.Models;
using Carpool.Interfaces;

namespace Carpool.Services
{
    public  class CarpoolService: CarpoolServiceInterface
    {
        private string Email = String.Empty;
        private CarpoolDBContext context;
        public CarpoolService()
        {
           context = new CarpoolDBContext();
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
            try
            {
                context.Users.Add(Newuser);
                context.SaveChanges();
                return "done succcessfully";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public List<OfferedRide> AvailableRides(DateTime date, string time, string fromplace, string toplace, int seats)
        {
            List<OfferedRide> res = new();
            bool flag;
            toplace=toplace.ToLower();
            string[] stops;
            try
            {

                foreach (OfferedRide Ride in context.OfferedRides)
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
                User? user = context.Users.SingleOrDefault(o => o.Uname.TrimEnd() == email && o.Password.TrimEnd() == password);
                if(user == null)
                {
                    return "invalid user id";
                }
                else
                {   
                    Email = email;
                    return "Logged in successfully ";         
                }
            }
            catch(Exception ex)
            { return ex.Message; }


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
                try
                {
                    context.OfferedRides.Add(newride);
                    context.SaveChanges();
                }
                catch(Exception e)
                { return e.Message; }

                return "done successfully";
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
                    OfferedRide? Ride = context.OfferedRides.SingleOrDefault(o => o.OfferId == offerid);
                    if (Ride != null)
                    {
                        BookedRide booking = new BookedRide()
                        {
                            OfferId = offerid,
                            Seats = seats,
                            BookedBy = Email
                        };


                        booking.Price = Ride.Price * seats;
                        Ride.Seats -= seats;
                        context.BookedRides.Add(booking);
                        context.OfferedRides.Update(Ride);
                        context.SaveChanges();
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

            foreach (OfferedRide Ride in context.OfferedRides)
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
            List<BookedRide> tres=new List<BookedRide>();   

            foreach (BookedRide Ride in context.BookedRides)
            {
                if (Ride.BookedBy.TrimEnd() == Email)
                {
                    tres.Add(Ride);
                }
            }
            foreach(BookedRide Ride in tres)
                    res.Add(context.OfferedRides.SingleOrDefault(o=>o.OfferId==Ride.OfferId));
            return res;

        }
    }
}
