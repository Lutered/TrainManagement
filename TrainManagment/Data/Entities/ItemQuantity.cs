namespace TrainManagment.Data.Entities
{
    public class ItemQuantity
    {
        public int ItemId { get; set; }
        public required Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
