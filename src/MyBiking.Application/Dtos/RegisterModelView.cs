using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Dtos
{
    public class RideModelView
    {
        public string SelectedDate => RideActivities.LastOrDefault()?.Year;
        public List<RideTimeActivity> RideActivities{ get; set; }
    }
}
