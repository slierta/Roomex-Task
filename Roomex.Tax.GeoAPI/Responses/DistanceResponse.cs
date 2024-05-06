using Roomex.Task.Core;
using Roomex.Task.GeoAPI.Requests;

namespace Roomex.Task.GeoAPI.Responses;
/// <summary>
/// Response for <see cref="DistanceRequest"/>
/// </summary>
public class DistanceResponse(double distance, DistanceMeasure measure = DistanceMeasure.Kilometers)
{
    /// <summary>
    /// Number of <see cref="DistanceMeasure"/> between two points
    /// </summary>
    public double Distance { get; init; } = distance;

    /// <summary>
    /// Measure used
    /// </summary>
    public DistanceMeasure Measure { get; init; } = measure;
}