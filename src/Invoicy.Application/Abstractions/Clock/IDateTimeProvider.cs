namespace Invoicy.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}
