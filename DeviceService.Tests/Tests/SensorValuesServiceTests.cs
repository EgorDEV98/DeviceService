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

public class SensorValuesServiceTests
{
    private readonly DeviceServiceDbContextMock _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ISensorValuesService _service;

    public SensorValuesServiceTests()
    {
        _context = DeviceMock.Create();
        _dateTimeProvider = new DateTimeProvider();
        _service = new SensorValuesService(_context, new SensorValuesServiceMapper());

        Seed();
    }

    #region GetSensorValue

    [Fact]
    public async Task GetSensorValue_Success()
    {
        var result = await _service.GetSensorValueAsync(new GetSensorValueParams()
        {
            Id = new Guid("9CB1BF2C-3B94-4C75-B627-98314970C41A")
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            result.Id.Should().Be(new Guid("9CB1BF2C-3B94-4C75-B627-98314970C41A"));
        }
    }
    
    [Fact]
    public async Task GetSensorValue_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.GetSensorValueAsync(new GetSensorValueParams()
            {
                Id = Guid.NewGuid()
            }, CancellationToken.None);
        });
    }

    #endregion

    #region GetSensorValues

    [Fact]
    public async Task GetSensorValues_Success()
    {
        var result = await _service.GetSensorValuesAsync(new GetSensorValuesParams()
        {
            
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            result.Count().Should().Be(2);
        }
    }
    
    [Fact]
    public async Task GetSensorValues_Params_Success()
    {
        var result = await _service.GetSensorValuesAsync(new GetSensorValuesParams()
        {
            Limit = 1
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            result.Count().Should().Be(1);
        }
    }
    
    #endregion

    #region AddSensorValue

    [Fact]
    public async Task AddSensorValue_Success()
    {
        var sensorId = new Guid("942AEA4D-8A15-4EF5-A621-DDF8915B2403");
        var totalBefore = await _context.SensorValues
            .Where(x => x.SensorId == sensorId)
            .CountAsync();
        
        var result = await _service.AddSensorValueAsync(new AddSensorValueParams()
        {
            SensorId = sensorId,
            Value = 3.3F,
            MeasurementDate = DateTime.Now
        }, CancellationToken.None);

        var totalCount = await _context.SensorValues
            .Where(x => x.SensorId == sensorId)
            .CountAsync();

        using (new AssertionScope())
        {
            totalCount.Should().BeGreaterThan(totalBefore);
        }
    }
    
    [Fact]
    public async Task AddSensorValue_SensorNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.AddSensorValueAsync(new AddSensorValueParams()
            {
                SensorId = Guid.NewGuid(),
                Value = 3.3F,
                MeasurementDate = DateTime.Now
            }, CancellationToken.None);
        });
    }

    #endregion

    #region RemoveSensorValue

    [Fact]
    public async Task RemoveSensorValue_Success()
    {
        await _service.DeleteSensorValueAsync(new DeleteSensorValueParams()
        {
            Id = new Guid("9CB1BF2C-3B94-4C75-B627-98314970C41A")
        }, CancellationToken.None);

        var isExistEntity = await _context.SensorValues
            .AnyAsync(x => x.Id == new Guid("9CB1BF2C-3B94-4C75-B627-98314970C41A"));

        using (new AssertionScope())
        {
            isExistEntity.Should().BeFalse();
        }
    }
    
    [Fact]
    public async Task RemoveSensorValue_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.DeleteSensorValueAsync(new DeleteSensorValueParams()
            {
                Id = Guid.NewGuid()
            }, CancellationToken.None);
        });
    }

    #endregion

    #region TruncateSensorValues

    [Fact]
    public void TruncateSensorValues_Success()
    {
        // Не поддерживается в InMemory
    }
    
    [Fact]
    public async Task TruncateSensorValues_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.TruncateSensorValuesAsync(new TruncateSensorValuesParams()
            {
                SensorId = Guid.NewGuid(),
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
                    SensorValues = new List<SensorValue>()
                    {
                        new()
                        {
                            Id = new Guid("9CB1BF2C-3B94-4C75-B627-98314970C41A"),
                            SensorId = new Guid("942AEA4D-8A15-4EF5-A621-DDF8915B2403"),
                            Value = 1.1F,
                            MeasurementDate = currentTime,
                        },
                        new()
                        {
                            Id = new Guid("2361D13E-247A-48C2-903D-1DE672771097"),
                            SensorId = new Guid("942AEA4D-8A15-4EF5-A621-DDF8915B2403"),
                            Value = 2.2F,
                            MeasurementDate = currentTime,
                        },
                    }
                }
            }
        };
        
        _context.Devices.Add(device);
        _context.SaveChanges();
    }
}