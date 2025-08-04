using MediatR;
using TrainManagement.DTOs;

namespace TrainManagement.MediatR.Commands
{
    public record class UpdateComponentCommand : IRequest<Result<bool>>
    {
        public ComponentIdDTO Component { get; init; }
        public UpdateComponentCommand(ComponentIdDTO dto)
        {
            Component = dto;
        }
    }
}
