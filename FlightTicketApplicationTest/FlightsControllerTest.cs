using FilghtTicketApplication.Controllers;
using FilghtTicketApplication.Data;
using FilghtTicketApplication.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Drawing;

namespace FlightTicketApplicationTest
{
    [TestClass]
    public class FlightsControllerTest
    {
        #region"Instances"
        private ApplicationDbContext _context;

        private FlightsController _flightsController;
        #endregion

        [TestInitialize]
        public void PrimeTheTestVariables()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .Options;   
            _context = new ApplicationDbContext(options);

            var airline = new Airline
            {
                airlineID = 47,
                airlineName = "Dummy",
                airlineEmail = "Dummy@test.ca"
            };

            _context.Add(airline);

            for(int i = 0; i < 2; i++)
            {
                _context.Add(new Flight
                {
                    flightID = i,
                    flightName = "Dummy" + i,
                    airlineID = 47,
                    airline = airline,
                    departureDate = System.DateTime.Now,
                    landingTime = System.DateTime.Now,
                    departureFrom = "La La land",
                    arrivalAt = "Gotham",
                    seats = null
                });
            }

            _context.SaveChanges();

            _flightsController = new FlightsController(_context);
        }

        #region"Index"
        [TestMethod]
        public void IndexLoadsView()
        {
            var result = (ViewResult)_flightsController.Index().Result;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexLoadsFlights()
        {
            var result = (ViewResult)_flightsController.Index().Result;

            List<Flight> flights = (List<Flight>)result.Model;

            CollectionAssert.AreEqual(_context.Flight.ToList(), flights);
        }
        #endregion

        #region"Details"
        [TestMethod]
        public void DetailsThrows404WhenIdIsNull()
        {
            var result = (ViewResult)_flightsController.Details(null).Result;

            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsThrows404WhenIdIsInvalid()
        {
            var result = (ViewResult)_flightsController.Details(-1).Result;

            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsLoadsViewWhenIdIsValid()
        {
            var result = (ViewResult)_flightsController.Details(1).Result;

            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void DetailsLoadsFlightWhenIdIsValid()
        {
            var result = (ViewResult)_flightsController.Details(1).Result;
            var flight = (Flight)result.Model;

            Assert.AreEqual(_context.Flight.Find(1), flight);
        }
        #endregion

        #region"Create"
        [TestMethod]
        public void CreateLoadsView()
        {
            var result = (ViewResult)_flightsController.Create();

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void CreateLoadsAirlines()
        {
            var result = (ViewResult)_flightsController.Create();

            //converted to string since comparing them as List didnt work out
            //even when using JSON to use the same data to parse a comparision list
            var airline = result.ViewData["airlineID"].ToString();
            var expected = new SelectList(_context.Set<Airline>(), "airlineID", "airlineName").ToString();

            Assert.AreEqual(expected, airline);
        }
        #endregion

        #region"Edit"
        #region"Edit [GET]"
        [TestMethod]
        public void EditThrows404WhenIdIsNull()
        {
            var result = (ViewResult)_flightsController.Edit(null).Result;

            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditThrows404WhenIdIsInvalid()
        {
            var result = (ViewResult)_flightsController.Edit(-1).Result;

            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditLoadsViewWhenIdIsValid()
        {
            var result = (ViewResult)_flightsController.Edit(1).Result;

            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditLoadFlightWhenIdIsValid()
        {
            var result = (ViewResult)_flightsController.Edit(1).Result;
            var flight = (Flight)result.Model;

            Assert.AreEqual(_context.Flight.Find(1), flight);
        }
        #endregion

        #region"Edit [POST]"
        [TestMethod]
        public void EditThrows404WhenGivenIdDoesntMatch()
        {
            var flight = _context.Flight.Find(1);
            var result = (ViewResult)_flightsController.Edit(-1, flight).Result;

            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditGoesToIndexWhenChangesAreSaved()
        {
            var flight = _context.Flight.Find(1);
            flight.flightName = "Dummy47";
            var result = (RedirectToActionResult)_flightsController.Edit(1, flight).Result;

            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void EditDoesntSaveInvalidModel()
        {
            var flight = _context.Flight.Find(1);
            flight.flightName = "";
            _flightsController.ModelState.AddModelError("flightName", "Flight Name cannot be empty...");

            var result =  (ViewResult)_flightsController.Edit(1, flight).Result;

            Assert.AreEqual("Edit", result.ViewName);
        }
        #endregion
        #endregion
    }
}