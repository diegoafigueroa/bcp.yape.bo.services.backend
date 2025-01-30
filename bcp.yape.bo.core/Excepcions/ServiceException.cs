using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcp.yape.bo.core.Excepcions
{
    /// <summary>
    /// A generic exception class for handling errors across various services and layers.
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class.
        /// </summary>
        public ServiceException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class with a specified message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ServiceException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class with a specified message and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception that caused this exception.</param>
        public ServiceException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Optionally, additional context data (like error codes or extra details) can be added to the exception.
        /// </summary>
        /// <param name="context">Additional context to provide further details about the error.</param>
        public ServiceException(string message, Exception innerException, string context)
            : base(message, innerException)
        {
            Context = context;
        }

        /// <summary>
        /// Gets or sets additional context information related to the error.
        /// </summary>
        public string Context { get; set; }
    }
}
