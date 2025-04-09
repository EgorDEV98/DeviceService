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

public class SensorServiceTests
{
    private readonly DeviceServiceDbContextMock _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ISensorsService _service;

    public SensorServiceTests()
    {
        _context = DeviceMock.Create();
        _dateTimeProvider = new DateTimeProvider();
        _service = new SensorsService(_context, new DateTimeProvider(), new SensorServiceMapper());

        Seed();
    }

    #region GetSensor

    [Fact]
    public async Task GetSensor_Success()
    {
        var result = await _service.GetSensorAsync(new GetSensorParams()
        {
            Id = new Guid("942AEA4D-8A15-4EF5-A621-DDF8915B2403")
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            result.Id.Should().Be(new Guid("942AEA4D-8A15-4EF5-A621-DDF8915B2403"));
        }
    }
    
    [Fact]
    public async Task GetSensor_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.GetSensorAsync(new GetSensorParams()
            {
                Id = Guid.NewGuid()
            }, CancellationToken.None);
        });
    }

    #endregion

    #region GetSensors

    [Fact]
    public async Task GetSensors_Success()
    {
        var result = await _service.GetSensorsAsync(new GetSensorsParams(), CancellationToken.None);
        using (new AssertionScope())
        {
            result.Count.Should().Be(2);
        }
    }
    
    [Fact]
    public async Task GetSensors_WithParams_Success()
    {
        var result = await _service.GetSensorsAsync(new GetSensorsParams()
        {
            Limit = 1
        }, CancellationToken.None);
        using (new AssertionScope())
        {
            result.Count.Should().Be(1);
        }
    }

    #endregion

    #region AddSensor

    [Fact]
    public async Task AddSensor_Success()
    {
        var allSensorsCountBefore = await _context.Sensors
            .Where(x => x.DeviceId == new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"))
            .AsNoTracking()
            .CountAsync();
        
        var result = await _service.AddSensorAsync(new AddSensorParams()
        {
            Name = "NEW_SENSOR",
            MeasurementSymbol = "NEW_SYMBOL",
            DeviceId = new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D")
        }, CancellationToken.None);
        
        var allSensorsCountAfter = await _context.Sensors
            .Where(x => x.DeviceId == new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"))
            .AsNoTracking()
            .CountAsync();

        using (new AssertionScope())
        {
            allSensorsCountAfter.Should().BeGreaterThan(allSensorsCountBefore);
            result.CreatedDate.Should().Be(result.LastUpdate);
            result.Name.Should().Be("NEW_SENSOR");
            result.MeasurementSymbol.Should().Be("NEW_SYMBOL");
        }
    }
    
    [Fact]
    public async Task AddSensor_NotFoundDevice()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.AddSensorAsync(new AddSensorParams()
            {
                Name = "NEW_SENSOR",
                MeasurementSymbol = "NEW_SYMBOL",
                DeviceId = Guid.NewGuid()
            }, CancellationToken.None);
        });
    }

    #endregion

    #region UpdateSensor

    [Fact]
    public async Task UpdateSensor_Success()
    {
        var sensorId = new Guid("942AEA4D-8A15-4EF5-A621-DDF8915B2403");
        
        await _service.UpdateSensorAsync(new UpdateSensorParams()
        {
            Id = sensorId,
            Name = "NEW_SENSOR_NAME",
        }, CancellationToken.None);

        var entity = await _context.Sensors
            .FirstOrDefaultAsync(x => x.Id == sensorId);

        using (new AssertionScope())
        {
            entity!.Name.Should().Be("NEW_SENSOR_NAME");
        }
    }
    
    [Fact]
    public async Task UpdateSensor_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.UpdateSensorAsync(new UpdateSensorParams()
            {
                Id = Guid.NewGuid(),
                Name = "NEW_SENSOR_NAME",
            }, CancellationToken.None);
        });
    }

    #endregion

    #region DeleteSensor

    [Fact]
    public async Task DeleteSensor_Success()
    {
        var sensorId = new Guid("942AEA4D-8A15-4EF5-A621-DDF8915B2403");
        await _service.DeleteSensorAsync(new DeleteSensorParams()
        {
            Id = sensorId
        }, CancellationToken.None);
        
        var isExist = await _context.Sensors.AnyAsync(x => x.Id == sensorId);

        using (new AssertionScope())
        {
            isExist.Should().BeFalse();
        }
    }
    
    [Fact]
    public async Task DeleteSensor_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.DeleteSensorAsync(new DeleteSensorParams()
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
            UserId = new Guid("301E74EA-FEF1-4B88-BF39-F6BA3871FAE0"),
            Sensors = new List<Sensor>()
            {
                new()
                {
                    Id = new Guid("942AEA4D-8A15-4EF5-A621-DDF8915B2403"),
                    Name = "TEST_SENSOR",
                    DeviceId = new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"),
                    MeasurementSymbol = "MEASUREMENT_SYMBOL",
                    CreatedDate = currentTime,
                    LastUpdate = currentTime,
                },
                new()
                {
                    Id = new Guid("591B4834-6E6B-4DE7-88B8-6B6855FF8810"),
                    Name = "TEST_SENSOR_2",
                    DeviceId = new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"),
                    MeasurementSymbol = "MEASUREMENT_SYMBOL_2",
                    CreatedDate = currentTime,
                    LastUpdate = currentTime,
                }
            }
        };
        
        _context.Devices.Add(device);
        _context.SaveChanges();
    }
}