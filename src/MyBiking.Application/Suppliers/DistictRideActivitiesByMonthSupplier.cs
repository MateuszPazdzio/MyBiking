using MyBiking.Application.Functions.Query.Ride;
using MyBiking.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Suppliers
{
    public class DistictRideActivitiesByMonthSupplier : IDistictRideActivitiesSupplier
    {
        public List<DateTime> GetDistinctRideActivitiesByMonth(List<Entity.Models.Ride> rides, int? latestYear)
        {
            if (!latestYear.HasValue)
            {
                return new List<DateTime>();
            }

            return rides.Where(r => r.StartingDateTime.Year == latestYear)
                   .Select(r => r.StartingDateTime)
                   .DistinctBy(r => r.ToString("MMMM"))
                   .OrderBy(r => r, new RideTimeActivityDatesComparer())
                   .ToList();
        }
    }

    internal class RideTimeActivityDatesComparer : IComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y)
        {
            return x.CompareTo(y);
        }
    }
}
