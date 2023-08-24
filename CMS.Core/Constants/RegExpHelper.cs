namespace Quivyo.Core.Constants
{
    /// <summary>
    /// Regular Expressions constants
    /// </summary>
    public static class RegExpHelper
    {

        /// <summary>
        /// Example : dd/MM/yyyy
        /// </summary>
        public const string Date = @"^([0]?[0-9]|[12][0-9]|[3][01])[/]([0]?[1-9]|[1][0-2])[/]([0-9]{4}|[0-9]{2})$";

        /// <summary>
        /// DecimalNumber
        /// </summary>
        public const string DecimalNumber = @"^\d{1,10}(?:\.\d{0,2})?$";

        /// <summary>
        /// FieldName
        /// </summary>
        public const string FieldName = @"^(?<![^\s+*/-])\w+(?![^\s+*/-])$";
    }
}
