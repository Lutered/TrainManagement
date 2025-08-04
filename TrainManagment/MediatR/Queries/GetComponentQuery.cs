using MediatR;
using TrainManagement.DTOs;

namespace TrainManagement.MediatR.Queries
{
    public record class GetComponentQuery : IRequest<Result<ComponentIdDTO>>
    {
        public int? Id { get; init; } = null;
        public string UniqueNumber { get; init; } = string.Empty;

        public GetComponentQuery(int id)
        {
            Id = id;
        }

        public GetComponentQuery(string uid)
        {
            UniqueNumber = uid;
        }
    }
}
