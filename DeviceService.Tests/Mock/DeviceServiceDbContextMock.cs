using DeviceService.Data;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Tests.Mock;

public class DeviceServiceDbContextMock : DeviceServiceDbContext
{
    public DeviceServiceDbContextMock()
        : base(new DbContextOptions<DeviceServiceDbContext>()) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }
}