using System.ComponentModel.DataAnnotations;

namespace TrainManagement.DTOs
{
    public record class ComponentDTO
    {
        [Required]
        public string Name { get; init; } = string.Empty;
        [Required]
        public string UniqueNumber { get; init; } = string.Empty;
        public bool CanAssignQuantity { get; init; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be less than 0")]
        public int Quantity { get; init; }
    }
}
