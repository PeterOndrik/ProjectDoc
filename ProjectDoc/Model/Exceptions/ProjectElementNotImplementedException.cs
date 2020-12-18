using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ProjectDoc.Model.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a requested method or operation is not implemented.
    /// </summary>
    public class ProjectElementNotImplementedException : ProjectElementException
    {
        /// <summary>
        /// Initializes a new instance of the ProjectElementNotImplementedException class.
        /// </summary>
        public ProjectElementNotImplementedException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProjectElementNotImplementedException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ProjectElementNotImplementedException(String message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProjectElementNotImplementedException class with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. 
        /// If the inner parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.
        /// </param>
        public ProjectElementNotImplementedException(String message, Exception innerException)
            :base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProjectElementNotImplementedException class with a specified source object, method and additional information.
        /// </summary>
        /// <param name="source">The source object of exception.</param>
        /// <param name="method">The source method of exception.</param>
        /// <param name="addition">The additional information.</param>
        public ProjectElementNotImplementedException(Object source, MethodBase method, String addition)
            : base("Object: " + source.GetType().Name + Environment.NewLine
                + "Method: " + method.ReflectedType.FullName + "." + method.Name + Environment.NewLine
                + "Addition: " + addition + Environment.NewLine)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProjectElementNotImplementedException with a specified source object and method.
        /// </summary>
        /// <param name="source">The source object of exception.</param>
        /// <param name="method">The source method of exception.</param>
        public ProjectElementNotImplementedException(Object source, MethodBase method)
            : this(source, method, String.Empty)
        {
        }
    }
}
