using System;
using System.Runtime.Serialization;

namespace CurrencyApi.Application.Exceptions
{
    /// <summary>
    /// Represents errors that occur during after fetching null or empty data from the data provider
    /// </summary>
    [Serializable]
    public class RecordNotFoundException : WebAppException
    {
        /// <summary>
        /// Initializes a new instance of the Exception class.
        /// </summary>
        public RecordNotFoundException() : base("No record was found.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RecordNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="messageFormat">The exception message format.</param>
        /// <param name="args">The exception message arguments.</param>
        public RecordNotFoundException(string messageFormat, params object[] args) : base(string.Format(messageFormat, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected RecordNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message and a reference to the inner
        /// exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner
        /// exception is specified.
        /// </param>
        public RecordNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
