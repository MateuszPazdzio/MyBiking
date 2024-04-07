using MyBiking.Application.Dtos;
using System.Globalization;

namespace MyBiking.Application.ViewModels
{
        public class MonthlyRideModelView
        {
                public List<RideDto> RideDtos { get; set; }
                public string Month => RideDtos.Count()>0 ? RideDtos[0].StartingDateTime.ToString("MMMM", new CultureInfo("en-US")) : string.Empty;
                public string Year => RideDtos.Count()>0 ? RideDtos[0].StartingDateTime.ToString("yyyy") : string.Empty;
        }
}