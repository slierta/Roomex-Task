using FluentAssertions;
using Roomex.Task.Core;

namespace Roomex.Task.GeoCalculator.Tests;

[TestClass]
public class DistanceCalculatorTests
{
    private IDistanceCalculator? _calculator=null;

    [TestInitialize]
    public void Initialize()
    {
        _calculator = new DistanceCalculator();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _calculator = null;
    }

    [TestMethod]
    public void WhenValuesAreZeroShouldReturnZero()
    {
        var x = new GeoLocation();
        var y=new GeoLocation();
        var result = _calculator.CalculateDistance(x, y);
        result.Should().Be(0);
    }
    /// <summary>
    /// Test the distance between two different points.
    /// Used https://gps-coordinates.org/distance-between-coordinates.php to check different values
    /// </summary>
    /// <param name="originLatitude"></param>
    /// <param name="originLongitude"></param>
    /// <param name="targetLatitude"></param>
    /// <param name="targetLongitude"></param>
    /// <param name="expectedMeters"></param>
    [DataTestMethod]
    [DataRow(35.6897, 139.6922, 19.4333, -99.1333, 11305831.21)]    // Tokio - Mexico City
    [DataRow(28.6100, 77.2300, 40.6943, -73.9249, 11754749.99)]    // Delhi - New York
    public void Calculate(
        double originLatitude, 
        double originLongitude, 
        double targetLatitude, 
        double targetLongitude,
        double expectedMeters)
    {
        var source = new GeoLocation() { Latitude = originLatitude, Longitude = originLongitude };
        var target = new GeoLocation() { Latitude = targetLatitude, Longitude = targetLongitude };
        var result =Math.Round(_calculator!.CalculateDistance(source, target,DistanceMeasure.Meters), 2);

        result.Should().Be(expectedMeters);
    }

    [TestMethod]
    public void ShouldRaiseExceptionWhenFirstValueIsInvalid()
    {
        var x = new GeoLocation() {Latitude = -111,Longitude = 0};
        var y = new GeoLocation();

        var action = () => _calculator!.CalculateDistance(x, y);

        action.Should().Throw<ArgumentException>().WithParameterName("x");
    }
    [TestMethod]
    public void ShouldRaiseExceptionWhenSecondValueIsInvalid()
    {
        var x = new GeoLocation() { Latitude = -1, Longitude = 0 };
        var y = new GeoLocation(){Longitude = 39,Latitude = 200};

        var action = () => _calculator!.CalculateDistance(x, y);

        action.Should().Throw<ArgumentException>().WithParameterName("y");
    }

    [DataTestMethod]
    [DataRow(28.6100, 77.2300, 40.6943, -73.9249, 11754749.99, DistanceMeasure.Meters)]     // Delhi - New York
    [DataRow(28.6100, 77.2300, 40.6943, -73.9249, 11754.75, DistanceMeasure.Kilometers)]    // Delhi - New York
    [DataRow(28.6100, 77.2300, 40.6943, -73.9249, 7304.06, DistanceMeasure.Miles)]          // Delhi - New York
    [DataRow(28.6100, 77.2300, 40.6943, -73.9249, 6347.06, DistanceMeasure.NauticalMiles)]  // Delhi - New York
    public void DistanceCalculatorShouldReturnDifferentMeasuresCorrectly(
        double originLatitude,
        double originLongitude,
        double targetLatitude,
        double targetLongitude,
        double expectedValue,
        DistanceMeasure measure)
    {
        var source = new GeoLocation() { Latitude = originLatitude, Longitude = originLongitude };
        var target = new GeoLocation() { Latitude = targetLatitude, Longitude = targetLongitude };
        var result = Math.Round(_calculator!.CalculateDistance(source, target, measure), 2);

        result.Should().Be(expectedValue);
    }
}