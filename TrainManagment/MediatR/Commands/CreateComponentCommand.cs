using MediatR;
using TrainManagement.DTOs;

namespace TrainManagement.MediatR.Commands
{
    public record class CreateComponentCommand : IRequest<Result<bool>>
    {
        public ComponentDTO Component { get; init; }
        public CreateComponentCommand(ComponentDTO dto)
        {
            Component = dto;
        }
    }
}
