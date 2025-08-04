using MediatR;

namespace TrainManagement.MediatR.Commands
{
    public record class DeleteComponentCommand : IRequest<Result<bool>>
    {
        public int? Id { get; init; } = null;
        public string UniqueNumber { get; init; } = string.Empty;

        public DeleteComponentCommand(int id)
        {
            Id = id;
        }

        public DeleteComponentCommand(string uid)
        {
            UniqueNumber = uid;
        }
    }
}
