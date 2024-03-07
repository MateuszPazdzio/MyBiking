using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Entity.Models
{
    public class RideTimeActivity
    {
        public string Year;
        public List<DateTime> RideTimeActivitiesDates;
        //public List<DateTime> SortedAscRideTimeActivitiesDates => RideTimeActivitiesDates.OrderBy(r=>r,new RideTimeActivityDatesComparer());
    }

    //internal class RideTimeActivityDatesComparer : IComparer<DateTime>
    //{
    //    public int Compare(DateTime x, DateTime y)
    //    {
    //        return x.CompareTo(y);
    //    }
    //}
}
