namespace TrainManagment.Data.Entities
{
    public class ItemQuality
    {
        public int ItemId { get; set; }
        public required Item Item { get; set; }
        public int Quality { get; set; }
    }
}
