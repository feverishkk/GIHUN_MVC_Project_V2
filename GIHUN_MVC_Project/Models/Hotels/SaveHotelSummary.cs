namespace GIHUN_MVC_Project.Models.Hotels
{
    public class SaveHotelSummary
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? HotelAddress { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public Summary? Json_OG { get; set; }
        public string? AddedDate { get; set; }
    }
}
