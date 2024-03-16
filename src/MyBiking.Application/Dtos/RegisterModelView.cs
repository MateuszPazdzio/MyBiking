using MyBiking.Application.Ride;

namespace MyBiking.Application.Dtos
{
    public class RideModelView
    {
        //public string SelectedDate => RideActivities.LastOrDefault()?.Year;
        public List<RideTimeActivity> RideActivities{ get; set; }
        public List<int> Years {  get; set; }
    }
}
