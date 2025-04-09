using CommonLib.Other.DateTimeProvider;

namespace DeviceService.Tests.Mock;

public class FakeDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset GetCurrentOffset() => new(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
    public DateTime GetCurrent() => new DateTime(2025, 1, 1, 0,0, 0);
}