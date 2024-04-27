using AutoMapper;
using Moq;
using MyBiking.Application.Dtos;
using MyBiking.Application.Mapper;
using MyBiking.Entity.IRepository;
using MyBiking.Entity.Models;
using MyBikingApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Tests.Mapper
{
    [TestFixture()]
    public class AutoMapperProfileTest
    {
        private IMapper mapper;
        private string userId;
        [SetUp]
        public void SetUp()
        {
            Mock<IUserHttpContext> _userHttpContext = new Mock<IUserHttpContext> ();

            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new AutoMapperProfile(_userHttpContext.Object)));

            userId = Guid.NewGuid().ToString();

            _userHttpContext.Setup(m => m.GetUser()).Returns(() => new CurrentUser()
            {
                Id = userId,
            });

            mapper = configuration.CreateMapper();
        }

        [Test()]
        public void Map_RideDtoToRide_Correctly()
        {
            //arrange
            RideDto rideDto = new RideDto()
            {
                Creation_Date = DateTime.Now,
            };

            //act
            var ride = mapper.Map<Entity.Models.Ride>(rideDto);

            //assert
            Assert.That(ride, Is.TypeOf<Entity.Models.Ride>());
            Assert.That(ride.ApplicationUserId == userId);
            Assert.That(ride.ApplicationUserId == userId);
        }

        [Test()]
        public void Map_WheelieItemDtoToWheelieItem_Correctly()
        {
            //arrange
            WheelieItemDto wheelieItemDto = new WheelieItemDto()
            {
                Address = "Street 15",
                Latitude = 52.21,
                Longitude = 48.51            
            };

            //act
            var ride = mapper.Map<WheelieItem>(wheelieItemDto);

            //assert
            Assert.That(ride, Is.TypeOf<WheelieItem>());
            Assert.That(ride.WheelePoint.Address == wheelieItemDto.Address);
            Assert.That(ride.WheelePoint.Latitude == wheelieItemDto.Latitude);
            Assert.That(ride.WheelePoint.Longitude == wheelieItemDto.Longitude);
        }
    }
}
