namespace CMS.Core.Models
{
    /// <summary>
    /// Response object for APIs
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class ApiResponse<TModel> where TModel : class
    {
        /// <summary>
        /// Represent the unique id for the request
        /// </summary>
        public string RequestId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Represent the data model
        /// </summary>
        public TModel Data { get; set; }

        /// <summary>
        /// Represent a list of errors
        /// </summary>
        public List<ApiErrorDto> Errors { get; set; } = new();

        /// <summary>
        /// Setup the request id for the response
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public ApiResponse<TModel> WithRequestId(string requestId)
        {
            RequestId = requestId;

            return this;
        }

        /// <summary>
        /// Create api response object with success result
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse<TModel> Success(TModel data = null)
        {
            return new()
            {
                Data = data,
                Errors = new(),
            };
        }

        /// <summary>
        /// Create api response object with error result
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse<TModel> Error(string key, string message)
        {
            return new()
            {
                Data = null,
                Errors = new() { new ApiErrorDto(key, message) },
            };
        }

        /// <summary>
        /// Create api response object with error result
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse<TModel> Error(string message)
        {
            return Error("general", message);
        }

        /// <summary>
        /// Create api response object with error result
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static ApiResponse<TModel> Error(List<ApiErrorDto> errors)
        {
            return new()
            {
                Data = null,
                Errors = errors,
            };
        }

        /// <summary>
        /// Create api response object with error result
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static ApiResponse<TModel> Error(Dictionary<string, string> errors)
        {
            return new()
            {
                Data = null,
                Errors = errors.Select(x => new ApiErrorDto(x.Key, x.Value)).ToList(),
            };
        }

        /// <summary>
        /// Operator to convert any model to api response of that model
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ApiResponse<TModel>(TModel value)
        {
            return Success(value);
        }
    }

    /// <summary>
    /// Response object for APIs
    /// </summary>
    public class ApiResponse : ApiResponse<object>
    {
        /// <summary>
        /// Create api response object with success result
        /// </summary>
        /// <returns></returns>
        public static ApiResponse Success()
        {
            return new()
            {
                Data = null,
                Errors = new(),
            };
        }
    }
}
