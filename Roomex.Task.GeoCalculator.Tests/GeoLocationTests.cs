using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Roomex.Task.Core;

namespace Roomex.Task.GeoCalculator.Tests
{
    [TestClass]
    public class GeoLocationTests
    {
        /// <summary>
        /// Check if the values for latitude and longitude are checked
        /// <remarks>
        /// The value for Latitude must be always between -90 and +90
        /// The value for Longitude must be always between -180 and +180
        /// </remarks>
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="numErrors"></param>
        [DataTestMethod()]
        [DataRow(0, 0, 0)]
        [DataRow(-90, 0, 0)]
        [DataRow(-91, 0, 1)]
        [DataRow(-91, -200, 2)]
        [DataRow(-90, -180, 0)]
        [DataRow(90, 180.0001, 1)]
        public void ValidateDataGeoLocation(double latitude,double longitude,int numErrors)
        {
            var expectedValid = numErrors == 0;
            var loc= new GeoLocation() { Latitude = latitude, Longitude = longitude };
            var res = new List<ValidationResult>();
            var isValid= Validator.TryValidateObject(loc,new ValidationContext(loc,null,null),res,true);
            isValid.Should().Be(expectedValid);
            res.Count.Should().Be(numErrors);
        }

       
    }
}