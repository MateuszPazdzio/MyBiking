using MyBiking.Application.Dtos;
using MyBiking.Application.ViewModels;
using MyBiking.Entity.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Tests.ViewModels
{
    [TestFixture]
    public class MonthlyRideModelViewTest
    {

        [Test]
        public void Month_WithRideDtos_ReturnsCorrectMonth()
        {
            // Arrange
            var rideDtos = new List<RideDto>
        {
            new RideDto { StartingDateTime = new DateTime(2024, 4, 10) }
            // Add more RideDto objects for different months if needed
        };
            var monthlyRideModelView = new MonthlyRideModelView { RideDtos = rideDtos };

            // Act
            var month = monthlyRideModelView.Month;

            // Assert
            Assert.That("April", Is.EqualTo(month));
        }

        [Test]
        public void Month_WithNoRideDtos_ReturnsEmptyString()
        {
            // Arrange
            var monthlyRideModelView = new MonthlyRideModelView { RideDtos = new List<RideDto>() };

            // Act
            var month = monthlyRideModelView.Month;

            // Assert
            Assert.That(string.Empty, Is.EqualTo(month));
        }

        [Test]
        public void Year_WithRideDtos_ReturnsCorrectYear()
        {
            // Arrange
            var rideDtos = new List<RideDto>
            {
                new RideDto { StartingDateTime = new DateTime(2024, 4, 10) }
                // Add more RideDto objects for different years if needed
            };
            var monthlyRideModelView = new MonthlyRideModelView { RideDtos = rideDtos };

            // Act
            var year = monthlyRideModelView.Year;

            // Assert
            Assert.That("2024", Is.EqualTo(year));
        }

        [Test]
        public void Year_WithNoRideDtos_ReturnsEmptyString()
        {
            // Arrange
            var monthlyRideModelView = new MonthlyRideModelView { RideDtos = new List<RideDto>() };

            // Act
            var year = monthlyRideModelView.Year;

            // Assert
            Assert.That(string.Empty, Is.EqualTo(year));
        }
    }
}
