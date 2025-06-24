using Invoicy.Application.Abstractions.Clock;

namespace Invoicy.Api.Tests.Fakes
{
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get; set; }
    }
}
