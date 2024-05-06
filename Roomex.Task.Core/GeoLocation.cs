using System.ComponentModel.DataAnnotations;

namespace Roomex.Task.Core;
/// <summary>
/// Geographical location
/// </summary>
public class GeoLocation:IValidatableObject
{
    /// <summary>
    /// Latitude value
    /// </summary>
    [Required]
    [Range(type: typeof(double), minimum: "-90", maximum: "90")]
    public double Latitude { get; init; }
    /// <summary>
    /// Longitude value
    /// </summary>
    [Required]
    [Range(type: typeof(double), minimum: "-180", maximum: "180")]
    public double Longitude { get; init; } = 0;


    /// <summary>Determines whether the specified object is valid.</summary>
    /// <param name="validationContext">The validation context.</param>
    /// <returns>A collection that holds failed-validation information.</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateProperty(Latitude, new ValidationContext(this, null, null) { MemberName = nameof(Latitude) }, results);
        Validator.TryValidateProperty(Latitude, new ValidationContext(this, null, null) { MemberName = nameof(Longitude) }, results);
        return results;
    }
}