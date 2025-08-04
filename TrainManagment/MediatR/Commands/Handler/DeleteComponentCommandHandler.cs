using AutoMapper;
using MediatR;
using TrainManagement.Interfaces;

namespace TrainManagement.MediatR.Commands.Handler
{
    public class DeleteComponentCommandHandler(
        IComponentRepository _componentRepository)
        : IRequestHandler<
            DeleteComponentCommand,
            Result<bool>>
        {
            public async Task<Result<bool>> Handle(DeleteComponentCommand request, CancellationToken cancellationToken)
            {
                var component = request.Id is not null ? 
                    await _componentRepository.GetComponentAsync((int)request.Id, cancellationToken) :
                    await _componentRepository.GetComponentAsync(request.UniqueNumber, cancellationToken);

                if (component == null)
                    return request.Id is not null ?
                        Result<bool>.Failure($"Component with Id - {request.Id} does not exists") :
                        Result<bool>.Failure($"Component with Unique number - {request.UniqueNumber} does not exists");

                _componentRepository.RemoveComponent(component);

                if (await _componentRepository.SaveChangesAsync()) return Result<bool>.Success(true); ;

                return Result<bool>.Failure("Failed to delete Component");
        }
    }
}
