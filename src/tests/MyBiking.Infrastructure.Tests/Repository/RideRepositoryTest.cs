using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using MyBiking.Application.Dtos;
using MyBiking.Entity.IRepository;
using MyBiking.Entity.Models;
using MyBiking.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Infrastructure.Tests.Repository
{
    [TestFixture()]
    internal class RideRepositoryTest
    {
        private Mock<IUserHttpContext> _mockUserHttpContext;
        private Mock<MyBikingDbContext> _mockMyBikingDbContext;
        private List<Ride> _fakeRides;
        [SetUp]
        public void SetUp()
        {
            SetRides();
            _mockUserHttpContext = new Mock<IUserHttpContext>();
            _mockMyBikingDbContext = new Mock<MyBikingDbContext>();
            _mockMyBikingDbContext.Setup(d => d.Rides).ReturnsDbSet(_fakeRides);
        }
        private void SetRides()
        {
            _fakeRides = new List<Ride>();
            _fakeRides.Add(new Ride
            {
                Id = 1,
                Creation_Date = DateTime.Now,
                Distance = 10,
                IsPublic = true,
                StartingDateTime = DateTime.Now.AddHours(1),
                EndingDateTime = DateTime.Now.AddHours(2),
                ApplicationUser = null,
                WheeleRides = new List<WheelieRide>
                {
                    new WheelieRide
                    {
                        Id = 1,
                        Distance = 37,
                        DurationTime = "34",
                        EndingDateTime= DateTime.Now.AddMinutes(65),
                        StartingDateTime= DateTime.Now.AddMinutes(65),
                        Ride = null,
                        RideId = 1,
                        WheeleItems = new List<WheelieItem>
                        {
                            new WheelieItem
                            {
                                Id = 1,
                                Address = "Test Street 207",
                                Altitude = 156,
                                Distance = 4,
                                MaxRotateX = 0.34,
                                Speed = 15,
                                WheelieRideId = 1,
                                WheelieRide = null,
                                WheelePoint = new WheeliePoint
                                {
                                    Address = "Fake Street 202",
                                    Latitude = 56.21,
                                    Longitude = 48.43,
                                    
                                }
                            }
                        }
                    }
                },
                ApplicationUserId = Guid.NewGuid().ToString(),
                Points = new List<Point>
                {
                    new Point
                    {
                        Id = 1,
                        Address = "Fake Street1",
                        Latitude = 48.25,
                        Longitude = 52.1245,
                        Ride = null,
                        RideId= 1,
                    },
                    new Point
                    {
                        Id = 1,
                        Address = "Fake Street2",
                        Latitude = 49.55,
                        Longitude = 42.145,
                        Ride = null,
                        RideId= 1,
                    },
                    new Point
                    {
                        Id = 1,
                        Address = "Fake Street3",
                        Latitude = 46.64,
                        Longitude = 56.45,
                        Ride = null,
                        RideId= 1,
                    },
                }
                
            });
        }

        [Test()]
        public async Task CreateRide_RideIsNull_ReturnsHttp400()
        {
            //arrange
            IRideRepository rideRepository = new RideRepository(_mockMyBikingDbContext.Object,
                _mockUserHttpContext.Object);
            Ride ride = null;

            //act
            var status =await rideRepository.CreateRide(ride);

            //assert
            Assert.That(status.Code == Entity.Enums.Code.HTTP400);
        }
        [Test()]
        public async Task CreateRide_CreatesRide_ReturnsHttp201()
        {
            //arrange
            IRideRepository rideRepository = new RideRepository(_mockMyBikingDbContext.Object,
                _mockUserHttpContext.Object);

            _mockMyBikingDbContext.Setup(m => m.Rides.Add(It.IsAny<Ride>()))
                .Callback<Ride>((c) => _fakeRides.Add(c));

            _mockMyBikingDbContext.Setup(m => m.SaveChanges())
                .Returns(() =>
                {
                    return _fakeRides.Count;
                });

            Ride ride = new Ride();
            //act
            var status = await rideRepository.CreateRide(ride);
            var savedRideCount = _mockMyBikingDbContext.Object.SaveChanges();

            //assert
            Assert.That(status.Code == Entity.Enums.Code.HTTP201);
            Assert.That(savedRideCount, Is.EqualTo(2));
        }


        [Test()]
        public async Task DeleteRide_DeletingExsistingRide_ReturnsHttp204()
        {
            //arrange
            IRideRepository rideRepository = new RideRepository(_mockMyBikingDbContext.Object,
                _mockUserHttpContext.Object);

            _mockMyBikingDbContext.Setup(m => m.Rides.Remove(It.IsAny<Ride>()))
                .Callback<Ride>((c) => _fakeRides.Remove(c));

            int rideId = 1;

            //act
            var status = await rideRepository.DeleteRide(rideId);
            var savedRideCount = _mockMyBikingDbContext.Object.SaveChanges();

            //assert
            Assert.That(status.Code == Entity.Enums.Code.HTTP204);
            Assert.That(savedRideCount, Is.EqualTo(0));
        }

        [Test()]
        public async Task DeleteRide_RideDoesNotExists_ReturnsHttp400()
        {
            //arrange
            IRideRepository rideRepository = new RideRepository(_mockMyBikingDbContext.Object,
                _mockUserHttpContext.Object);

            _mockMyBikingDbContext.Setup(m => m.Rides.Remove(It.IsAny<Ride>()))
                .Callback<Ride>((c) => _fakeRides.Remove(c));


            int rideId = 5;

            //act
            var status = await rideRepository.DeleteRide(rideId);
            var savedRideCount = _mockMyBikingDbContext.Object.SaveChanges();

            //assert
            Assert.That(status.Code == Entity.Enums.Code.HTTP500);
            Assert.That(savedRideCount, Is.EqualTo(0));
            Assert.That(_fakeRides.Count, Is.EqualTo(1));
        }
    }
}
