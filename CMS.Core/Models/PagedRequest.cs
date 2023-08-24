namespace Quivyo.Core.Models
{
    /// <summary>
    /// Represent an object for request that has pagination
    /// </summary>
    public class PagedRequest
    {
        /// <summary>
        /// Return a default instance without pagination
        /// </summary>
        public static PagedRequest Default => new PagedRequest()
        {
            EnablePagination = false,
        };


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
        public string OrderBy { get; set; }

        /// <summary>
        /// Represent the name of the field to include in the search. You can add multiple fields with comma separated. (Ex: "FirstName,LastName")
        /// </summary>
        /// <example></example>
        public string SearchBy { get; set; }

        /// <summary>
        /// Represent the text that will search with.
        /// </summary>
        /// <example></example>
        public string SearchText { get; set; }

        /// <summary>
        /// Represent whether return all data without pagination or use pagination
        /// </summary>
        /// <example>true</example>
        public bool EnablePagination { get; set; } = true;

        /// <summary>
        /// Represent how to draw the grid (Specified for data tables)
        /// </summary>
        public string Draw { get; set; }
    }
}
