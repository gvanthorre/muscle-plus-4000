using MediatR;

namespace MusclePlus4000.Domain.Common;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}