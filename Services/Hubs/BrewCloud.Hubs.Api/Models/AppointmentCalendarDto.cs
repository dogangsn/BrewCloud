namespace BrewCloud.Hubs.Api.Models
{
    public class AppointmentCalendarDto
    {
        public Guid id { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
