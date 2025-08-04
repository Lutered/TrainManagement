namespace TrainManagement.Params
{
    public class ComponentParams : PaginationParams
    {
        public string? Name { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public string OrderBy { get; set; } = "id";
    }
}
