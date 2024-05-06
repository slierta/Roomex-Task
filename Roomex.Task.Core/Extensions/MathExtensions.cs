namespace Roomex.Task.Core.Extensions;
/// <summary>
/// Math extensions used 
/// </summary>
public static class MathExtensions
{
    /// <summary>
    /// Converts a number into his radian
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static double ConvertToRadian(this double value)=>value * (Math.PI / 180);
}