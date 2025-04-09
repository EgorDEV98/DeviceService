using CommonLib.Exceptions;
using CommonLib.Other.DateTimeProvider;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Mappers;
using DeviceService.Application.Models.Params;
using DeviceService.Application.Services;
using DeviceService.Data.Entities;
using DeviceService.Tests.Mock;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Tests.Tests;

public class DeviceServiceTests
{
    private readonly DeviceServiceDbContextMock _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDevicesService _service;

    public DeviceServiceTests()
    {
        _context = DeviceMock.Create();
        _dateTimeProvider = new DateTimeProvider();
        _service = new DevicesService(_context, new DeviceServiceMapper(), new DateTimeProvider());

        Seed();
    }

    #region GetDevice

    [Fact]
    public async Task GetDevice_Success()
    {
        var result = await _service.GetDeviceAsync(new GetDeviceParams()
        {
            Id = new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D")
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            result.Id.Should().Be(new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"));
            result.Name.Should().Be("TEST_DEVICE_1");
        }
    }

    [Fact]
    public async Task GetDevice_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.GetDeviceAsync(new GetDeviceParams()
            {
                Id = Guid.NewGuid()
            }, CancellationToken.None);
        });
    }

    #endregion

    #region GetDevices

    [Fact]
    public async Task GetDevices_WithoutParams_Success()
    {
        var result = await _service.GetDevicesAsync(new GetDevicesParams(), CancellationToken.None);
        using (new AssertionScope())
        {
            result.Count.Should().Be(3);
        }
    }
    
    [Fact]
    public async Task GetDevices_WithUserParams_Success()
    {
        var result = await _service.GetDevicesAsync(new GetDevicesParams()
        {
            UserIds = [new Guid("301E74EA-FEF1-4B88-BF39-F6BA3871FAE0")]
        }, CancellationToken.None);
        using (new AssertionScope())
        {
            result.Count.Should().Be(2);
        }
    }
    
    [Fact]
    public async Task GetDevices_WithLimit_Success()
    {
        var result = await _service.GetDevicesAsync(new GetDevicesParams()
        {
            Limit = 2
        }, CancellationToken.None);
        using (new AssertionScope())
        {
            result.Count.Should().Be(2);
        }
    }

    #endregion

    #region AddDevice

    [Fact]
    public async Task AddDevice_Success()
    {
        var result = await _service.AddDeviceAsync(new AddDeviceParams()
        {
            Name = "NEW_TEST_DEVICE",
            UserId = new Guid("301E74EA-FEF1-4B88-BF39-F6BA3871FAE0")
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            result.Name.Should().Be("NEW_TEST_DEVICE");
            result.LastUpdate.Should().Be(result.CreatedDate);
        }
    } 
    
    #endregion
    
    #region UpdateDevice

    [Fact]
    public async Task UpdateDevice_Success()
    {
        var result = await _service.UpdateDeviceAsync(new UpdateDeviceParams()
        {
            Id = new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"),
            Name = "NEW_TEST_DEVICE",
        }, CancellationToken.None);
        
        var updatedDevice = await _context.Devices
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == result.Id);

        using (new AssertionScope())
        {
            updatedDevice!.Name.Should().Be("NEW_TEST_DEVICE");
            updatedDevice.LastUpdate.Should().BeAfter(result.CreatedDate);
            updatedDevice.Id.Should().Be(new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"));
        }
    }
    
    [Fact]
    public async Task UpdateDevice_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.UpdateDeviceAsync(new UpdateDeviceParams()
            {
                Id = Guid.NewGuid(),
                Name = "NEW_TEST_DEVICE",
            }, CancellationToken.None);
        });
    }
    
    #endregion
    
    #region DeleteDevice

    [Fact]
    public async Task DeleteDevice_Success()
    {
        var deviceId = new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D");

        var result = await _service.DeleteDeviceAsync(new DeleteDeviceParams()
        {
            Id = deviceId
        }, CancellationToken.None);
        
        var device = await _context.Devices
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == deviceId);

        using (new AssertionScope())
        {
            result.Should().BeTrue();
            device.Should().BeNull();
        }
    }
    
    [Fact]
    public async Task DeleteDevice_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.DeleteDeviceAsync(new DeleteDeviceParams()
            {
                Id = Guid.NewGuid()
            }, CancellationToken.None);
        });
    }
    
    #endregion
    
    private void Seed()
    {
        // Первое устройство пользователя
        var currentTime = _dateTimeProvider.GetCurrent();
        var device = new Device()
        {
            Id = new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"),
            Name = "TEST_DEVICE_1",
            CreatedDate = currentTime,
            LastUpdate = currentTime,
            UserId = new Guid("301E74EA-FEF1-4B88-BF39-F6BA3871FAE0")
        };
        
        // Второе устройство пользователя
        var device2 = new Device()
        {
            Id = new Guid("782144C4-8854-44B8-A016-C7405879F2D0"),
            Name = "TEST_DEVICE_2",
            CreatedDate = currentTime,
            LastUpdate = currentTime,
            UserId = new Guid("301E74EA-FEF1-4B88-BF39-F6BA3871FAE0")
        };
        
        // Устройство другого пользователя
        var device3 = new Device()
        {
            Id = new Guid("777961C7-84DF-46BC-884F-C581ED8F6291"),
            Name = "TEST_DEVICE_3",
            CreatedDate = currentTime,
            LastUpdate = currentTime,
            UserId = Guid.NewGuid()
        };
        
        _context.Devices.AddRange(device, device2, device3);
        _context.SaveChanges();
    }
}