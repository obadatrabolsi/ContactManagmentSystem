namespace CMS.Core.Models
{
    /// <summary>
    /// Represent an object for request that has pagination
    /// </summary>
    public class PagedRequest
    {
        /// <summary>
        /// Represent the page index to start pagination from. (0 is the default)
        /// </summary>
        ///<example>0</example>
        public int PageIndex { get; set; }

        /// <summary>
        /// Represent the size of each page for the pagination. (10 is the default)
        /// </summary>
        ///<example>10</example>
        public int PageSize { get; set; }

        /// <summary>
        /// Represent the property name\s to order the result. 
        /// You can send the field name to order the data ascending or 
        /// you append '-' to field name to sort the data descending.
        /// You can send multiples fields (ex: -id,name) 
        /// </summary>
        /// <example>-Id</example>
        public string[] OrderBy { get; set; }

        /// <summary>
        /// Represent a list of key value pairs for data filtration, the key represent the field name the value represent the key value
        /// </summary>
        public Dictionary<string, object> SearchFields { get; set; } = new Dictionary<string, object>();

    }
}
