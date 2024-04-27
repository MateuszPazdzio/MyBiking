using Moq;
using MyBiking.Application.Functions.Query.Ride;
using MyBiking.Application.Interfaces;
using MyBiking.Application.Ride;
using MyBiking.Entity.IRepository;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Tests.Functions.Query.Ride
{
    [TestFixture]
    public class RideTimeActivityQueryHandlerTest
    {
        private Mock<IRideRepository> _mockRideRepository;
        private Mock<IDistictRideActivitiesSupplier> _mockDistinctRideActivitySupplier;
        private List<Entity.Models.Ride> _fakeRides;

        [SetUp]
        public void SetUp()
        {
            _mockRideRepository = new Mock<IRideRepository>();
            _mockDistinctRideActivitySupplier = new Mock<IDistictRideActivitiesSupplier>();
            SetRides();
        }
        private void SetRides()
        {
            _fakeRides = new List<Entity.Models.Ride>();
            _fakeRides.AddRange(new List<Entity.Models.Ride>(){
                new Entity.Models.Ride
                    {
                    Id = 1,
                    Creation_Date = DateTime.Now.AddDays(-1),
                    Distance = 10,
                    IsPublic = true,
                    StartingDateTime = DateTime.Now.AddYears(-1),
                    EndingDateTime = DateTime.Now,
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
                new Entity.Models.Ride
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
        [Test()]
        public async Task Handle_RideActivityIsNull_ReturnsNull()
        {
            //arrange
            var rideActivity = new RideTimeActivityQuery()
            {
                Year = null
            };
            var handler = new RideTimeActivityQueryHandler(_mockRideRepository.Object, _mockDistinctRideActivitySupplier.Object);

            var response = _mockRideRepository.Setup(m => m.GetRideActivitiesSelectedByYear(It.IsAny<int?>()))
                .ReturnsAsync(() => null);

            //act
            var result = await handler.Handle(rideActivity, CancellationToken.None);

            //assert
            Assert.That(result, Is.Null);
        }

        [Test()]
        public async Task Handle_RideTimeActivityQueryIsNull_ReturnsNull()
        {
            //arrange
            RideTimeActivityQuery rideActivity = null;
            var handler = new RideTimeActivityQueryHandler(_mockRideRepository.Object, _mockDistinctRideActivitySupplier.Object);

            //act
            var result = await handler.Handle(rideActivity, CancellationToken.None);

            //assert
            Assert.That(result, Is.Null);
        }

        [Test()]
        public async Task Handle_YearIsSpecified_RetrunsRideTimeActivity()
        {
            //arrange
            var rideActivityQuery = new RideTimeActivityQuery()
            {
                Year = 2024
            };

            var handler = new RideTimeActivityQueryHandler(_mockRideRepository.Object, _mockDistinctRideActivitySupplier.Object);

            var supplier = _mockDistinctRideActivitySupplier.Setup(m => m.GetDistinctRideActivitiesByMonth(It.IsAny<List<Entity.Models.Ride>>(), It.IsAny<int?>()))
                .Returns(() => new List<DateTime>()
                {
                    DateTime.Now,
                    DateTime.Now
                });

            var rideActivities = _mockRideRepository.Setup(m => m.GetRideActivitiesSelectedByYear(It.IsAny<int?>()))
               .ReturnsAsync(() => _fakeRides);

            //act
            var result = await handler.Handle(rideActivityQuery, CancellationToken.None);

            //assert
            Assert.That(result.RideTimeActivitiesDates.Count() == 2);
        }

        [Test()]
        public async Task Handle_FindsYearsOfRideActivities_RetrunsLatestRideTimeActivity()
        {
            //arrange
            var rideActivityQuery = new RideTimeActivityQuery()
            {
                Year = null
            };

            var handler = new RideTimeActivityQueryHandler(_mockRideRepository.Object, _mockDistinctRideActivitySupplier.Object);

            var supplier = _mockDistinctRideActivitySupplier.Setup(m => m.GetDistinctRideActivitiesByMonth(It.IsAny<List<Entity.Models.Ride>>(), It.IsAny<int?>()))
                .Returns(() => new List<DateTime>()
                {
                    DateTime.Now,
                    DateTime.Now
                });

            var rideActivities = _mockRideRepository.Setup(m => m.GetRideActivitiesSelectedByYear(It.IsAny<int?>()))
               .ReturnsAsync(() => _fakeRides);

            var lastYear = _fakeRides.DistinctBy(r => r.StartingDateTime.Year)
                    .Select(rd => rd.StartingDateTime.Year)
                    .OrderByDescending(rd => rd)
                    .ToList()
                    .Max();
            //act
            var result = await handler.Handle(rideActivityQuery, CancellationToken.None);

            //assert
            Assert.That(result.RideTimeActivitiesDates[0].Year == lastYear);
        }
        [Test()]
        public async Task Handle_DoesNotFindsAnyYearsOfRideActivities_RetrunsLatestRideTimeActivityWithEmptyDates()
        {
            //arrange
            var rideActivityQuery = new RideTimeActivityQuery()
            {
                Year = null
            };

            var handler = new RideTimeActivityQueryHandler(_mockRideRepository.Object, _mockDistinctRideActivitySupplier.Object);

            var supplier = _mockDistinctRideActivitySupplier.Setup(m => m.GetDistinctRideActivitiesByMonth(It.IsAny<List<Entity.Models.Ride>>(), It.IsAny<int?>()))
                .Returns(() => new List<DateTime>());

            var rideActivities = _mockRideRepository.Setup(m => m.GetRideActivitiesSelectedByYear(It.IsAny<int?>()))
               .ReturnsAsync(() => new List<Entity.Models.Ride>());

            var lastYear = _fakeRides.DistinctBy(r => r.StartingDateTime.Year)
                    .Select(rd => rd.StartingDateTime.Year)
                    .OrderByDescending(rd => rd)
                    .ToList()
                    .Max();
            //act
            var result = await handler.Handle(rideActivityQuery, CancellationToken.None);

            //assert
            Assert.That(result.RideTimeActivitiesDates.Count == 0);
        }
    }
}
