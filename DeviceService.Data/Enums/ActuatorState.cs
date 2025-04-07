using System.Runtime.Serialization;

namespace DeviceService.Data.Enums;

/// <summary>
/// Состояние
/// </summary>
public enum ActuatorState
{
    /// <summary>
    /// Выключено
    /// </summary>
    [EnumMember(Value = "DISABLE")]
    Disable,
    
    /// <summary>
    /// Включено
    /// </summary>
    [EnumMember(Value = "ENABLE")]
    Enable
}