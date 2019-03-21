namespace CarDealership.Models.ServiceModels
{
    public class CarSearchFilters
    {
        public string MakeModelYear { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}