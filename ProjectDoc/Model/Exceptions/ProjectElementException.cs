using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ProjectDoc.Model.Exceptions
{
    /// <summary>
    /// Represents the general project element exception.
    /// </summary>
    public class ProjectElementException : ApplicationException
    {
        #region Private Members

        /// <summary>
        /// The error message that explains the reason for the exception.
        /// </summary>
        private String _message;

        /// <summary>
        /// The project element for which the exception was threw.
        /// </summary>
        private IProjectElement _projectElement;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the error message that explains the reason for the exception.
        /// </summary>
        public override string Message
        {
            get
            {
                return _message;
            }
        }

        /// <summary>
        /// Gets the project element for which the exception was threw.
        /// </summary>
        public IProjectElement Element
        {
            get
            {
                return _projectElement;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ProjectElementException class.
        /// </summary>
        public ProjectElementException()
        {
            _message = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the ProjectElementException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ProjectElementException(String message)
        {
            _message = message;
        }

        /// <summary>
        /// Initializes a new instance of the ProjectElementException class with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. 
        /// If the inner parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.
        /// </param>
        public ProjectElementException(String message, Exception innerException)
            : base(message, innerException)
        {
            _message = message;
        }

        /// <summary>
        /// Initializes a new instance of the ProjectElementException class with a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception. 
        /// If the inner parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public ProjectElementException(Exception innerException)
            : this(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProjectElementException class with a project element and the inner exception.
        /// </summary>
        /// <param name="projectElement">The project element for which exception was threw.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. 
        /// If the inner parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public ProjectElementException(IProjectElement projectElement, Exception innerException)
            : this(projectElement.Name, innerException)
        {
            IProjectElement parent = projectElement.Parent as IProjectElement;
            String parentName = String.Empty;
            if (parent != null)
                parentName = parent.Name + ".";

            if (innerException is OverflowException)
            {
                _message = String.Format("Value of element {0}{1} in an overflow.", parentName, projectElement.Name);
            }
            else if (innerException.InnerException != null)
            {
                _message = innerException.InnerException.Message;
            }
            _projectElement = projectElement;
        }

        public ProjectElementException(Object source, MethodBase method, String addition)
            : this("Object: " + source.GetType().Name + Environment.NewLine
                + "Method: " + method.ReflectedType.FullName + "." + method.Name + Environment.NewLine
                + "Addition: " + addition + Environment.NewLine)
        {
        }

        #endregion
    }
}
