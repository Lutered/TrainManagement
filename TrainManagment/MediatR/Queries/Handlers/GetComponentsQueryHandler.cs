using AutoMapper;
using MediatR;
using TrainManagement.DTOs;
using TrainManagement.Helpers;
using TrainManagement.Interfaces;

namespace TrainManagement.MediatR.Queries.Handlers
{
    public class GetComponentsQueryHandler(
         IComponentRepository _componentRepository,
         IMapper _mapper)
         : IRequestHandler<
             GetComponentsQuery,
             Result<PagedList<ComponentIdDTO>>>
    {
        public async Task<Result<PagedList<ComponentIdDTO>>> Handle(GetComponentsQuery request, CancellationToken cancellationToken)
        {
            var records = await _componentRepository.GetComponentsAsync(request.Params, cancellationToken);

            return Result<PagedList<ComponentIdDTO>>.Success(new PagedList<ComponentIdDTO>(
                _mapper.Map<List<ComponentIdDTO>>(records),
                records.Count,
                records.CurrentPage,
                records.PageSize
            ));
        }
    }
}
