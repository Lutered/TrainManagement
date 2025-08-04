using AutoMapper;
using MediatR;
using TrainManagement.Data.Entities;
using TrainManagement.Interfaces;

namespace TrainManagement.MediatR.Commands.Handler
{
    public class CreateComponentCommandHandler(
        IComponentRepository _componentRepository,
        IMapper _mapper)
        : IRequestHandler<
            CreateComponentCommand,
            Result<bool>>
    {
        public async Task<Result<bool>> Handle(CreateComponentCommand request, CancellationToken cancellationToken)
        {
            var componentDTO = request.Component;

            if (await _componentRepository.IsExistsAsync(componentDTO.UniqueNumber, cancellationToken))
                return Result<bool>.Failure($"Component with unique number - {componentDTO.UniqueNumber} already exists");

            var recordToAdd = _mapper.Map<Component>(componentDTO);

            await _componentRepository.CreateItemAsync(recordToAdd, cancellationToken);
            if (await _componentRepository.SaveChangesAsync()) return Result<bool>.Success(true);

            return Result<bool>.Failure("Failed to create component");
        }
    }
}
