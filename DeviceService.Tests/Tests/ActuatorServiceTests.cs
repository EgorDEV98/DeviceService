using CommonLib.Exceptions;
using CommonLib.Other.DateTimeProvider;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Mappers;
using DeviceService.Application.Models.Params;
using DeviceService.Application.Services;
using DeviceService.Contracts.Enums;
using DeviceService.Data.Entities;
using DeviceService.Tests.Mock;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Tests.Tests;

public class ActuatorServiceTests
{
    private readonly DeviceServiceDbContextMock _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IActuatorsService _service;

    public ActuatorServiceTests()
    {
        _context = DeviceMock.Create();
        _dateTimeProvider = new DateTimeProvider();
        _service = new ActuatorsService(_context, new DateTimeProvider(), new ActuatorServiceMapper());

        Seed();
    }

    #region GetActuator

    [Fact]
    public async Task GetActuator_Success()
    {
        var result = await _service.GetActuatorAsync(new GetActuatorParams()
        {
            Id = new Guid("56A89F70-C3C5-4996-9F7B-DE9FDD41A8D7")
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            result.Id.Should().Be(new Guid("56A89F70-C3C5-4996-9F7B-DE9FDD41A8D7"));
            result.Name.Should().Be("TEST_ACTUATOR_1");
        }
    }
    
    [Fact]
    public async Task GetActuator_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.GetActuatorAsync(new GetActuatorParams()
            {
                Id = Guid.NewGuid()
            }, CancellationToken.None);
        });
    }

    #endregion

    #region GetActuators

    [Fact]
    public async Task GetActuators_WithoutParams_Success()
    {
        var result = await _service.GetActuatorsAsync(new GetActuatorsParams()
        {
            
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            result.Count.Should().Be(2);
        }
    }
    
    #endregion

    #region AddActuator

    [Fact]
    public async Task AddActuator_Success()
    {
        var actuatorsCountBefore = await _context.Actuators
            .AsNoTracking()
            .Where(x => x.DeviceId == new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"))
            .CountAsync();
        
        var result = await _service.AddActuatorAsync(new AddActuatorParams()
        {
            DeviceId = new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"),
            Name = "NEW_ACTUATOR"
        }, CancellationToken.None);
        
        var actuatorsCountAfter = await _context.Actuators
            .AsNoTracking()
            .Where(x => x.DeviceId == new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"))
            .CountAsync();

        using (new AssertionScope())
        {
            result.CreatedDate.Should().Be(result.LastUpdate);
            actuatorsCountAfter.Should().BeGreaterThan(actuatorsCountBefore);
        }
    }
    
    [Fact]
    public async Task AddActuator_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.AddActuatorAsync(new AddActuatorParams()
            {
                DeviceId = Guid.NewGuid(),
                Name = "NEW_ACTUATOR"
            }, CancellationToken.None);
        });
    }

    #endregion

    #region UpdateActuator

    [Fact]
    public async Task UpdateActuator_Success()
    {
        var actuatorId = new Guid("56A89F70-C3C5-4996-9F7B-DE9FDD41A8D7");

        var beforeUpdate = await _context.Actuators
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == actuatorId);
        
        await _service.UpdateActuatorAsync(new UpdateActuatorParams()
        {
            Id = actuatorId,
            Name = "UPDATE_TEST_ACTUATOR_1",
            State = ActuatorState.Enable
        }, CancellationToken.None);

        var afterUpdate = await _context.Actuators
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == actuatorId);

        using (new AssertionScope())
        {
            afterUpdate!.Name.Should().Be("UPDATE_TEST_ACTUATOR_1");
            afterUpdate.State.Should().Be(ActuatorState.Enable);
            afterUpdate.Name.Should().NotBe(beforeUpdate!.Name);
            afterUpdate.State.Should().NotBe(beforeUpdate.State);
        }
    }
    
    [Fact]
    public async Task UpdateActuator_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.UpdateActuatorAsync(new UpdateActuatorParams()
            {
                Id = Guid.NewGuid(),
                Name = "UPDATE_TEST_ACTUATOR_1",
                State = ActuatorState.Enable
            }, CancellationToken.None);
        });
    }

    #endregion

    #region DeleteActuator

    [Fact]
    public async Task DeleteActuator_Success()
    {
        var actuatorId = new Guid("56A89F70-C3C5-4996-9F7B-DE9FDD41A8D7");
        var result = await _service.DeleteAsync(new DeleteActuatorParams()
        {
            Id = actuatorId
        }, CancellationToken.None);

        var entityAfterDelete = await _context.Actuators
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == actuatorId);
        
        using (new AssertionScope())
        {
            result.Should().BeTrue();
            entityAfterDelete.Should().BeNull();
        }
    }
    
    [Fact]
    public async Task DeleteActuator_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _service.DeleteAsync(new DeleteActuatorParams()
            {
                Id = Guid.NewGuid(),
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
            Actuators = new List<Actuator>()
            {
                new()
                {
                    Id = new Guid("56A89F70-C3C5-4996-9F7B-DE9FDD41A8D7"),
                    DeviceId = new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"),
                    Name = "TEST_ACTUATOR_1",
                    State = ActuatorState.Disable,
                    CreatedDate = currentTime,
                    LastUpdate = currentTime
                },
                new()
                {
                    Id = new Guid("C3C405DA-0B23-4443-9466-BD6EA5F15674"),
                    DeviceId = new Guid("773DF18B-3B52-42CC-9686-0FBF8F63A08D"),
                    Name = "TEST_ACTUATOR_2",
                    State = ActuatorState.Enable,
                    CreatedDate = currentTime,
                    LastUpdate = currentTime
                }
            }
        };
        
        _context.Devices.Add(device);
        _context.SaveChanges();
    }
}