using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;

namespace CurrencyApi.Application.Exceptions
{
    public class IdentityResultException : WebAppException
    {
        private readonly List<IdentityError> _errors;

        /// <summary>
        /// An <see cref="IEnumerable{T}"/> of <see cref="IdentityError"/>s containing an errors
        /// that occurred during the identity operation.
        /// </summary>
        /// <value>An <see cref="IEnumerable{T}"/> of <see cref="IdentityError"/>s.</value>
        public IEnumerable<IdentityError> Errors => _errors;

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="errors">An <see cref="IEnumerable{T}"/> of <see cref="IdentityError"/>s containing an errors that occurred during the identity operation.</param>
        public IdentityResultException(IEnumerable<IdentityError> errors) => _errors = errors.ToList();

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="errors">An <see cref="IEnumerable{T}"/> of <see cref="IdentityError"/>s containing an errors that occurred during the identity operation.</param>
        /// <param name="message">The message that describes the error.</param>
        public IdentityResultException(IEnumerable<IdentityError> errors, string message) : base(message) => _errors = errors.ToList();

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="errors">An <see cref="IEnumerable{T}"/> of <see cref="IdentityError"/>s containing an errors that occurred during the identity operation.</param>
        /// <param name="messageFormat">The exception message format.</param>
        /// <param name="args">The exception message arguments.</param>
        public IdentityResultException(IEnumerable<IdentityError> errors, string messageFormat, params object[] args) : base(string.Format(messageFormat, args)) => _errors = errors.ToList();

        /// <summary>
        /// Initializes a new instance of the Exception class with serialized data.
        /// </summary>
        /// <param name="errors">An <see cref="IEnumerable{T}"/> of <see cref="IdentityError"/>s containing an errors that occurred during the identity operation.</param>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected IdentityResultException(IEnumerable<IdentityError> errors, SerializationInfo info, StreamingContext context) : base(info, context) => _errors = errors.ToList();

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message and a reference to the inner
        /// exception that is the cause of this exception.
        /// </summary>
        /// <param name="errors">An <see cref="IEnumerable{T}"/> of <see cref="IdentityError"/>s containing an errors that occurred during the identity operation.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner
        /// exception is specified.
        /// </param>
        public IdentityResultException(IEnumerable<IdentityError> errors, string message, Exception innerException) : base(message, innerException) => _errors = errors.ToList();
    }
}
