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
        private IRideRepository _rideRepository;

        #region setup
        [SetUp]
        public void SetUp()
        {
            SetRides();
            _mockUserHttpContext = new Mock<IUserHttpContext>();
            _mockMyBikingDbContext = new Mock<MyBikingDbContext>();
            _mockMyBikingDbContext.Setup(d => d.Rides).ReturnsDbSet(_fakeRides);
            _rideRepository = new RideRepository(_mockMyBikingDbContext.Object,
                _mockUserHttpContext.Object);
        }
        private void SetRides()
        {
            _fakeRides = new List<Ride>();
            _fakeRides.AddRange(new List<Ride>(){
                new Ride
                    {
                    Id = 1,
                    Creation_Date = DateTime.Now.AddDays(-1),
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

                },
                new Ride
                    {
                    Id = 2,
                    Creation_Date = DateTime.Now,
                    Distance = 40,
                    IsPublic = true,
                    StartingDateTime = DateTime.Now.AddHours(4),
                    EndingDateTime = DateTime.Now.AddHours(5),
                    ApplicationUser = null,
                    WheeleRides = new List<WheelieRide>
                    {
                        new WheelieRide
                        {
                            Id = 2,
                            Distance = 55,
                            DurationTime = "12",
                            EndingDateTime= DateTime.Now.AddMinutes(15),
                            StartingDateTime= DateTime.Now.AddMinutes(15),
                            Ride = null,
                            RideId = 1,
                            WheeleItems = new List<WheelieItem>
                            {
                                new WheelieItem
                                {
                                    Id = 2,
                                    Address = "Test Street 121",
                                    Altitude = 156,
                                    Distance = 4,
                                    MaxRotateX = 0.34,
                                    Speed = 15,
                                    WheelieRideId = 1,
                                    WheelieRide = null,
                                    WheelePoint = new WheeliePoint
                                    {
                                        Address = "Fake Street 151",
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
                            Id = 4,
                            Address = "Fake Street4",
                            Latitude = 48.25,
                            Longitude = 52.1245,
                            Ride = null,
                            RideId= 1,
                        },
                        new Point
                        {
                            Id = 5,
                            Address = "Fake Street5",
                            Latitude = 49.55,
                            Longitude = 42.145,
                            Ride = null,
                            RideId= 1,
                        },
                        new Point
                        {
                            Id = 6,
                            Address = "Fake Street6",
                            Latitude = 46.64,
                            Longitude = 56.45,
                            Ride = null,
                            RideId= 1,
                        },
                    }

                }
             }
            );
        }

        #endregion

        #region CreateRide
        [Test()]
        public async Task CreateRide_RideIsNull_ReturnsHttp400()
        {
            //arrange
            Ride ride = null;

            //act
            var status = await _rideRepository.CreateRide(ride);

            //assert
            Assert.That(status.Code == Entity.Enums.Code.HTTP400);
        }
        [Test()]
        public async Task CreateRide_CreatesRide_ReturnsHttp201()
        {
            //arrange

            _mockMyBikingDbContext.Setup(m => m.Rides.Add(It.IsAny<Ride>()))
                .Callback<Ride>((c) => _fakeRides.Add(c));

            _mockMyBikingDbContext.Setup(m => m.SaveChanges())
                .Returns(() =>
                {
                    return _fakeRides.Count;
                });

            Ride ride = new Ride();
            //act
            var status = await _rideRepository.CreateRide(ride);
            var savedRideCount = _mockMyBikingDbContext.Object.SaveChanges();

            //assert
            Assert.That(status.Code == Entity.Enums.Code.HTTP201);
            Assert.That(savedRideCount, Is.EqualTo(3));
        }
        #endregion

        #region DeleteRide
        [Test()]
        public async Task DeleteRide_DeletingExsistingRide_ReturnsHttp204()
        {
            //arrange

            _mockMyBikingDbContext.Setup(m => m.Rides.Remove(It.IsAny<Ride>()))
                .Callback<Ride>((c) => _fakeRides.Remove(c));

            int rideId = 1;

            //act
            var status = await _rideRepository.DeleteRide(rideId);
            var savedRideCount = _mockMyBikingDbContext.Object.SaveChanges();

            //assert
            Assert.That(status.Code == Entity.Enums.Code.HTTP204);
            Assert.That(savedRideCount, Is.EqualTo(0));
        }

        [Test()]
        public async Task DeleteRide_RideDoesNotExists_ReturnsHttp400()
        {
            //arrange

            _mockMyBikingDbContext.Setup(m => m.Rides.Remove(It.IsAny<Ride>()))
                .Callback<Ride>((c) => _fakeRides.Remove(c));


            int rideId = 5;

            //act
            var status = await _rideRepository.DeleteRide(rideId);
            var savedRideCount = _mockMyBikingDbContext.Object.SaveChanges();

            //assert
            Assert.That(status.Code == Entity.Enums.Code.HTTP500);
            Assert.That(savedRideCount, Is.EqualTo(0));
            Assert.That(_fakeRides.Count, Is.EqualTo(2));
        }
        #endregion

        #region GetRideActivitiesSelectedByYear

        [Test()]
        public async Task GetRideActivitiesSelectedByYear_YearIsNotSpecifiedAndUserIsLoggedIn_ReturnsRides()
        {
            //arrange
            var user = new CurrentUser()
            {
                Id = Guid.NewGuid().ToString(),
            };
            var userLoggedIn = _mockUserHttpContext.Setup(u => u.GetUser())
                .Returns(user);

            _fakeRides[0].ApplicationUserId = user.Id;
            _fakeRides[1].ApplicationUserId = user.Id;
            int? year = null;

            //act
            var result = await _rideRepository.GetRideActivitiesSelectedByYear(year);

            //assert
            Assert.That(result.Count, Is.EqualTo(2));
            //checks if rides have been sorted desc
            Assert.That(result[1].Id, Is.EqualTo(_fakeRides[0].Id));
        }

        [Test()]
        public async Task GetRideActivitiesSelectedByYear_UserIsNotLoggedIn_ThrowsException()
        {
            //arrange
            CurrentUser user = null;
            var userLoggedIn = _mockUserHttpContext.Setup(u => u.GetUser())
                .Returns(user);

            int? year = null;

            //assert
            Assert.That(async () => await _rideRepository.GetRideActivitiesSelectedByYear(year), Throws.Exception);
        }

        [Test()]
        public async Task GetRideActivitiesSelectedByYear_UserIsLoggedInAndYearIsSpecified_ReturnsRides()
        {
            //arrange
            var user = new CurrentUser()
            {
                Id = Guid.NewGuid().ToString(),
            };
            var userLoggedIn = _mockUserHttpContext.Setup(u => u.GetUser())
                .Returns(user);

            _fakeRides[0].ApplicationUserId = user.Id;
            _fakeRides[1].ApplicationUserId = user.Id;
            int? year = 2024;

            //act
            var result = await _rideRepository.GetRideActivitiesSelectedByYear(year);

            //assert
            Assert.That(result.Count, Is.EqualTo(2));
        }
        #endregion

        #region GetRidesByMonthAsync

        [Test()]
        public async Task GetRidesByMonthAsync_UserIsLoggedIn_ReturnsRides()
        {
            //arrange
            CurrentUser user = null;
            var userLoggedIn = _mockUserHttpContext.Setup(u => u.GetUser())
                .Returns(user);
            string year = "2024";
            string month = "January";

            //assert
            Assert.That(async () => await _rideRepository.GetRidesByMonthAsync(year, month), Is.Null);
        }

        [TestCase("2024",null)]
        [TestCase(null, "January")]
        [TestCase("2024","January")]
        public async Task GetRidesByMonthAsync_InvalidMonth_ReturnsNull(string year, string month)
        {
            //arrange
            CurrentUser user = null;
            var userLoggedIn = _mockUserHttpContext.Setup(u => u.GetUser())
                .Returns(user);

            //assert
            Assert.That(async () => await _rideRepository.GetRidesByMonthAsync(year, month), Is.Null);
        }
        #endregion
    }
}
