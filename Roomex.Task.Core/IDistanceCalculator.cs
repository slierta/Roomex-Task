namespace Roomex.Task.Core
{
    /// <summary>
    /// Declares methods for the calculator
    /// </summary>
    public interface IDistanceCalculator
    {
        /// <summary>
        /// Calculate the distance between two different points on Earth
        /// </summary>
        /// <param name="x">First point</param>
        /// <param name="y">Second point</param>
        /// <returns></returns>
        double CalculateDistance(GeoLocation x, GeoLocation y);
        /// <summary>
        /// Calculate the distance between two different points on Earth
        /// </summary>
        /// <param name="x">First point</param>
        /// <param name="y">Second point</param>
        /// <param name="measure"></param>
        /// <returns></returns>
        double CalculateDistance(GeoLocation x, GeoLocation y,DistanceMeasure measure);
    }
}
