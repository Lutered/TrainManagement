using AutoMapper;
using MediatR;
using TrainManagement.DTOs;
using TrainManagement.Interfaces;

namespace TrainManagement.MediatR.Queries.Handlers
{
    public class GetComponentQueryHandler(
        IComponentRepository _componentRepository, 
        IMapper _mapper) 
        : IRequestHandler<
            GetComponentQuery, 
            Result<ComponentIdDTO>>
    {
        public async Task<Result<ComponentIdDTO>> Handle(GetComponentQuery request, CancellationToken cancellationToken)
        {
            var component = request.Id is not null ? 
                    await _componentRepository.GetComponentAsync((int)request.Id, cancellationToken) : 
                    await _componentRepository.GetComponentAsync(request.UniqueNumber, cancellationToken);

            if (component == null) 
                return Result<ComponentIdDTO>.Failure("Component was not found");

            return Result<ComponentIdDTO>
                    .Success(_mapper.Map<ComponentIdDTO>(component));
        }
    }
}
