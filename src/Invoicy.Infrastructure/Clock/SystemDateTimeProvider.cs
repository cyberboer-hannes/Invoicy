using Invoicy.Application.Abstractions.Clock;

namespace Invoicy.Infrastructure.Clock
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
