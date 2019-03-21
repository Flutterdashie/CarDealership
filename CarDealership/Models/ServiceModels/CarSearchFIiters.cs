namespace CarDealership.Models.ServiceModels
{
    public class CarSearchFilters
    {
        public bool IsNew { get; set; }
        public string MakeModelYear { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}