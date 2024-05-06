using System.Text.Json.Serialization;

namespace Roomex.Task.Core;
/// <summary>
/// Different distance measures
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DistanceMeasure
{
    /// <summary>
    /// Returns the distance in meters
    /// </summary>
    Meters,
    /// <summary>
    /// Kilometers
    /// </summary>
    Kilometers,
    /// <summary>
    /// Land miles
    /// </summary>
    Miles,
    /// <summary>
    /// Sea miles
    /// </summary>
    NauticalMiles
}