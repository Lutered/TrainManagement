using MediatR;
using TrainManagement.DTOs;
using TrainManagement.Helpers;
using TrainManagement.Params;

namespace TrainManagement.MediatR.Queries
{
    public class GetComponentsQuery : IRequest<Result<PagedList<ComponentIdDTO>>>
    {
        public ComponentParams Params { get; set; }
        public GetComponentsQuery(ComponentParams componentParams) 
        {
            Params = componentParams;
        }
    }
}
