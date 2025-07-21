using System.ComponentModel.DataAnnotations;

namespace TrainManagment.DTOs
{
    public class ItemDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string UniqueNumber { get; set; } = string.Empty;
        public bool CanAssignQuantity { get; set; }
    }
}
