using System.ComponentModel.DataAnnotations;

namespace TrainManagment.DTOs
{
    public class ItemIdDTO : ItemDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
