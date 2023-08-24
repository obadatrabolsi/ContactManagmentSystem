namespace CMS.Core.Models
{
    /// <summary>
    /// Api error dto
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="Message"></param>
    public record ApiErrorDto(string Key, string Message);
}
