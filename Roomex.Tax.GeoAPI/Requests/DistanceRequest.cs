using Roomex.Task.Core;

namespace Roomex.Task.GeoAPI.Requests;
/// <summary>
/// Request to calculate the distance between two points
/// </summary>
public class DistanceRequest
{
    /// <summary>
    /// Point of origin
    /// </summary>
    public GeoLocation Origin { get; set; }
    /// <summary>
    /// Point of target
    /// </summary>
    public GeoLocation Target { get; set; }
    /// <summary>
    /// Indicates how the distance should be returned 
    /// </summary>
    public DistanceMeasure Measure { get; set; } = DistanceMeasure.Kilometers;
}