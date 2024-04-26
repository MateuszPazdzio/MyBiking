using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Interfaces
{
    public interface IDistictRideActivitiesSupplier
    {
        List<DateTime> GetDistinctRideActivitiesByMonth(List<Entity.Models.Ride> rides, int? latestYear);
    }
}
