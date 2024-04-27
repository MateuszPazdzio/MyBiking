using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Query.Ride;
using MyBiking.Entity.Constants;
using MyBiking.Entity.IRepository;
using MyBiking.Entity.Models;
using MyBiking.MVC.Controllers;
using MyBikingApi.Models.Dtos;
using NuGet.Packaging.Signing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.MVC.Tests.Controllers
{
    [TestFixture]
    public class RideControllerTest
    {
        private Mock<IMediator> _mockMediator;
        private Mock<IRideRepository> _mockRideRepository;
        private HttpClient _client;
        [SetUp]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();
            _client = new WebApplicationFactory<Program>().WithWebHostBuilder(webHostBuilder =>
                webHostBuilder.ConfigureServices(service => service.AddScoped(_ => _mockMediator.Object)))
                .CreateClient();
        }
        [TearDown]
        public void TearDown()
        {
            _mockMediator.VerifyAll();
            _client.Dispose();
        }

        [Test()]
        public async Task MonthlyRides_YearOfRideIsNotSetUp_ReturnsIndexView()
        {
            //Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<RideQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<RideDto>()
            {

            });
            var month = "April";
            var year = "2024";

            //Act
            var response = await _client.GetAsync($"/Ride/MonthlyRides/{month}?year={year}");
            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.That(responseContent.Contains("<label class=\"control-label\" for=\"Password\">Password</label>"));
        }

        [Test()]
        public async Task MonthlyRides_GetsListOfValidMonthlyRides_ReturnsMontlhyRidesView()
        {
            //Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<RideQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<RideDto>()
            {
                new RideDto()
                {
                    Id = 1,
                    StartingDateTime = new DateTime(2024,1,1),
                    EndingDateTime = new DateTime(2024,1,1),
                    Distance = 124,
                    WheeleRides =new List<WheelieRideDto>(){
                        new WheelieRideDto(),
                    }
                },
                new RideDto()
                {
                    Id = 2,
                    StartingDateTime = new DateTime(2024,1,1),
                    EndingDateTime = DateTime.Now,
                    Distance = 11.5,
                    WheeleRides =new List<WheelieRideDto>(){
                        new WheelieRideDto(),
                    }
                },
            });
            string month = "April";
            //Act
            var response = await _client.GetAsync($"/Ride/MonthlyRides/{month}");
            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.That(responseContent.Contains("Rides in January 2024"));
            Assert.That(responseContent,Does.Contain("11.5"));
        }
    }
}
