using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace babelhealth
{
    /// <summary>
    /// Model to return from the API with a status and messaging.
    /// </summary>
    public class ResponseModel
    {
        private Dictionary<string, string> _errors = new Dictionary<string, string>();
        private Dictionary<string, string> _warnings = new Dictionary<string, string>();

        /// <summary>
        /// Response status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusType Status { get; set; }

        /// <summary>
        /// Response message
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Errors in key-value form.
        /// Used to return multiple, targeted errors.
        /// </summary>
        /// <value></value>
        public Dictionary<string, string> Errors 
        {
            get { return _errors; }
            set { _errors = value; }
        }

        /// <summary>
        /// Warnings in key-value form.
        /// Used to return multiple, targeted warnings.
        /// </summary>
        /// <value></value>
        public Dictionary<string, string> Warnings
        {
            get { return _warnings; }
            set { _warnings = value; }
        }
    }

    /// <summary>
    /// ResponseModel with status, messaging and the addition of a typed data payload.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseModel<T> : ResponseModel where T : class
    {
        /// <summary>
        /// Response data to return to the client
        /// </summary>
        /// <value></value>
        public T Data {get; set;}
    }

    /// <summary>
    /// Status for an API response.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusType
    {
        Success,
        Failure,
        Warning
    }
}