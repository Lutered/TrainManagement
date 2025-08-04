using AutoMapper;
using MediatR;
using TrainManagement.Interfaces;

namespace TrainManagement.MediatR.Commands.Handler
{
    public class UpdateComponentCommandHandler(
        IComponentRepository _componentRepository,
        IMapper _mapper)
        : IRequestHandler<
            UpdateComponentCommand,
            Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateComponentCommand request, CancellationToken cancellationToken)
        {
            var componentDTO = request.Component;

            var component = await _componentRepository.GetComponentAsync(componentDTO.Id, cancellationToken);

            if (component == null)
                return Result<bool>.Failure($"Component with unique number - {componentDTO.UniqueNumber} does not exists");

             _mapper.Map(componentDTO, component);

            if (await _componentRepository.SaveChangesAsync()) return Result<bool>.Success(true);

            return Result<bool>.Failure("Failed to update component");
        }
    }
}
