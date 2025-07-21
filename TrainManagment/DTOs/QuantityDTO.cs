using System.ComponentModel.DataAnnotations;

namespace TrainManagment.DTOs
{
    public class QuantityDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be less than 0")]
        public int Quantity { get; set; }
    }
}
