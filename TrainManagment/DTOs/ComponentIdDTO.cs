using System.ComponentModel.DataAnnotations;

namespace TrainManagement.DTOs
{
    public record class ComponentIdDTO : ComponentDTO
    {
        [Required]
        public int Id { get; init; }
    }
}
