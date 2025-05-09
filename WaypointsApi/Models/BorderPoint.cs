namespace WaypointsApi.Models
{
    public class BorderPoint
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int WaypointId { get; set; }
    }
} 