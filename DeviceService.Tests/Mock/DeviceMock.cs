namespace DeviceService.Tests.Mock;

public static class DeviceMock
{
    public static DeviceServiceDbContextMock Create()
    {
        var reserveDbMock = new DeviceServiceDbContextMock();
        reserveDbMock.Database.EnsureDeleted();
        reserveDbMock.Database.EnsureCreated();
        reserveDbMock.SaveChanges();
        return reserveDbMock;
    }
}