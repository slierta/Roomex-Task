using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Roomex.Task.Core;
using Roomex.Task.GeoAPI.Requests;
using Roomex.Task.GeoAPI.Responses;

namespace Roomex.Task.API.Tests
{
    [TestClass]
    public class ExerciseControllerTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;
        private const string CalculateDistanceUri = "/api/exercise/CalculateDistance";

        [TestInitialize]
        public void Initialize()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task WhenTheRequestIsNullShouldReturnBadRequest()
        {
            DistanceRequest req = null!;
            var result = await _client.PostAsJsonAsync(CalculateDistanceUri, req);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task WhenTheRequestIsNotCompleteShouldReturnBadRequest()
        {
            var req = new DistanceRequest()
            {
                Origin = new GeoLocation()
            }!;
            var result = await _client.PostAsJsonAsync(CalculateDistanceUri , req);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async System.Threading.Tasks.Task WhenTheRequestIsNotCompleteTargetShouldReturnBadRequest()
        {
            var req = new DistanceRequest()
            {
                Target= new GeoLocation()
            }!;
            var result = await _client.PostAsJsonAsync(CalculateDistanceUri, req);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async System.Threading.Tasks.Task WhenAnyOfTheValuesInRequestIsNotCorrectShouldReturnBadRequest()
        {
            var req = new DistanceRequest()
            {
                Origin = new GeoLocation(){Latitude = -120},
                Target = new GeoLocation()
            }!;
            var result = await _client.PostAsJsonAsync(CalculateDistanceUri, req);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task WhenTheValuesAreZeroShouldReturnZeroAndKilometers()
        {
            var req = new DistanceRequest()
            {
                Origin = new GeoLocation() ,
                Target = new GeoLocation()
            }!;
            var result = await _client.PostAsJsonAsync(CalculateDistanceUri, req);
            
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await result.Content.ReadFromJsonAsync<DistanceResponse>();
            content.Should().NotBeNull();
            content!.Distance.Should().Be(0);
            content.Measure.Should().Be(DistanceMeasure.Kilometers);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task WhenTheValueIsCalculatedInMetersShouldReturnIt()
        {
            var distanceExpected = 11754749.99;
            var req = new DistanceRequest()
            {
                Origin = new GeoLocation(){Latitude = 28.6100 ,Longitude = 77.2300 },
                Target = new GeoLocation(){Latitude = 40.6943 ,Longitude = -73.9249 },
                Measure = DistanceMeasure.Meters
            }!;
            
            var result = await _client.PostAsJsonAsync(CalculateDistanceUri, req);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await result.Content.ReadFromJsonAsync<DistanceResponse>();
            var roundDistance = Math.Round(content.Distance, 2);

            content.Should().NotBeNull();
            content.Measure.Should().Be(DistanceMeasure.Meters);
            roundDistance.Should().Be(distanceExpected);
        }
    }
}