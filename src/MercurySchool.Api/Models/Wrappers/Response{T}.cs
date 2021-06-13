namespace MercurySchool.Api.Models.Wrappers
{
    /// <summary>
    /// The Response class.
    /// Contains properties to be returned with an <c>IActionResult</c> response.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

        /// <summary>
        /// Data returned.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Indicate if the request successful
        /// </summary>
        public bool Succeeded { get; init; }

        /// <summary>
        /// Communicate errors that may have been thrown.
        /// </summary>
        public string[] Errors { get; init; }

        /// <summary>
        /// Explanation of results.
        /// </summary>
        public string Message { get; init; }
    }
}