using System.ComponentModel.DataAnnotations;
using Roomex.Task.Core;
using Roomex.Task.Core.Extensions;

namespace Roomex.Task.GeoCalculator
{
    /// <summary>
    /// Implementation of <see cref="IDistanceCalculator"/>
    /// </summary>
    public class DistanceCalculator: IDistanceCalculator
    {
        /// <summary>
        /// Earth radius in Kilometers
        /// </summary>
        private static double RadiusInKilometers=6371;
        /// <summary>
        /// Calculate the distance between two different points on Earth and returns the value in meters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double CalculateDistance(GeoLocation x, GeoLocation y)
        {
            if (!IsValid(x, out var results))
                throw new ArgumentException(string.Concat(results.Select(s => s.ErrorMessage)), nameof(x));

            if(!IsValid(y,out var resultsY))
                throw new ArgumentException(string.Concat(resultsY.Select(s => s.ErrorMessage)), nameof(y));

            var radXLat = x.Latitude.ConvertToRadian();
            var radYLat = y.Latitude.ConvertToRadian();
            var lon = y.Longitude.ConvertToRadian() - x.Longitude.ConvertToRadian();
            var d3 = Math.Pow(Math.Sin((radYLat - radXLat) / 2.0), 2.0) + Math.Cos(radXLat) * Math.Cos(radYLat) * Math.Pow(Math.Sin(lon / 2.0), 2.0);

            return (RadiusInKilometers*1000) * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        /// <summary>
        /// Calculate the distance between two different points on Earth
        /// </summary>
        /// <param name="x">First point</param>
        /// <param name="y">Second point</param>
        /// <param name="measure"></param>
        /// <returns></returns>
        public double CalculateDistance(GeoLocation x, GeoLocation y, DistanceMeasure measure)
        {
            var distance = CalculateDistance(x, y);
            return measure switch
            {
                DistanceMeasure.Meters => distance,
                DistanceMeasure.Kilometers => distance / 1000,
                DistanceMeasure.Miles => distance / 1609.344,
                DistanceMeasure.NauticalMiles => distance / 1852,
                _ => throw new ArgumentOutOfRangeException(nameof(measure), measure, null),
            };
        }
        /// <summary>
        /// Check if the <see cref="GeoLocation"/> is valid and returns the list of errors if any
        /// </summary>
        /// <param name="value"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private bool IsValid(GeoLocation value,out ICollection<ValidationResult> results)
        {
            var errors = new List<ValidationResult>();
            _ = Validator.TryValidateObject(value, new ValidationContext(value, null, null), errors, true);
            results=errors;
            return errors.Count == 0;
        }
    }
}
