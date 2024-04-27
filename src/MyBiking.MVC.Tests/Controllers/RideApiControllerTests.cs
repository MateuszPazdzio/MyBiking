using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command.RideApi;
using MyBiking.Application.Functions.Query.Ride;
using MyBiking.Entity.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.MVC.Tests.Controllers
{
    [TestFixture]
    public class RideApiControllerTests
    {
        private Mock<IMediator> _mockMediator;
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
            //_mockMediator.VerifyAll();
            _client.Dispose();
        }
        [Test]
        public async Task CreateRide_UserUnAuthorized_Returns401()
        {
            //arrange
            Status status = new Status()
            {
                Code = Entity.Enums.Code.HTTP400,
            };
            var rideDto = new RideDtoApiCommand();

            var content = new StringContent(JsonConvert.SerializeObject(rideDto), Encoding.UTF8, "application/json");

            _mockMediator.Setup(m => m.Send(It.IsAny<RideDtoApiCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=> { return status; });
            //act
            var response =await _client.PostAsync("api/ride", content);
            //assert
            Assert.That(response.IsSuccessStatusCode,Is.False);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));

        }

        [Test]
        public async Task CreateRide_RideIsCreated_ReturnsBadRequest()
        {
            //arrange
            Status status = new Status()
            {
                Code = Entity.Enums.Code.HTTP400,
            };
            var rideDto = new RideDtoApiCommand();


            _mockMediator.Setup(m => m.Send(It.IsAny<RideDtoApiCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => { return status; });

            var content = new StringContent(JsonConvert.SerializeObject(rideDto), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2F" +
                "wLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImY5YTU1MTk5LTZlMjctNGNhOC04NWM3LTNmZDI0NzZkYTI1MCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub" +
                "3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Im1Ad3AucGwiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1l" +
                "IjoidXNlcm5hbWUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNZW1iZXIiLCJleHAiOjE3MTUwNzYwODMsImlzcyI6Imh0dHA" +
                "6Ly9teWJpa2luZy5wbCIsImF1ZCI6Imh0dHA6Ly9teWJpa2luZy5wbCJ9.stTULdUXZ1SVnB1aiD3Oa47MmtWzBSKLxk8rKRXCErw");

            //act
            var response = await _client.PostAsync("api/ride", content);
            //assert
            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task CreateRide_RideIsCreated_ReturnsOk()
        {
            //arrange
            Status status = new Status()
            {
                Code = Entity.Enums.Code.HTTP201,
            };
            var rideDto = new RideDtoApiCommand();


            _mockMediator.Setup(m => m.Send(It.IsAny<RideDtoApiCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => { return status; });

            var content = new StringContent(JsonConvert.SerializeObject(rideDto), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2F" +
                "wLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImY5YTU1MTk5LTZlMjctNGNhOC04NWM3LTNmZDI0NzZkYTI1MCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub" +
                "3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Im1Ad3AucGwiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1l" +
                "IjoidXNlcm5hbWUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNZW1iZXIiLCJleHAiOjE3MTUwNzYwODMsImlzcyI6Imh0dHA" +
                "6Ly9teWJpa2luZy5wbCIsImF1ZCI6Imh0dHA6Ly9teWJpa2luZy5wbCJ9.stTULdUXZ1SVnB1aiD3Oa47MmtWzBSKLxk8rKRXCErw");

            //act
            var response = await _client.PostAsync("api/ride", content);
            //assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
