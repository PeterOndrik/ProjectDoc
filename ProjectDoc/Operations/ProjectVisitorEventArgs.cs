using System;
using System.Collections.Generic;
using System.Text;

using ProjectDoc.Model;

namespace ProjectDoc.Operations
{
    /// <summary>
    /// Provides data for the events in the ProjectVisitor class.
    /// </summary>
    public class ProjectVisitorEventArgs : EventArgs
    {
        /// <summary>
        /// The value for max or progress.
        /// </summary>
        private Int32 _number;

        private String _message;

        private Exception _visitorException;

        private IProjectElement _processedElement;

        /// <summary>
        /// Gets the state of save progress.
        /// </summary>
        public Int32 Number
        {
            get
            {
                return _number;
            }
        }

        public String Message
        {
            get
            {
                return _message;
            }
        }

        public Exception VisitorException
        {
            get
            {
                return _visitorException;
            }
        }

        public IProjectElement ProcessedElement
        {
            get
            {
                return _processedElement;
            }
        }

        /// <summary>
        /// Initializes a new instance of the ProjectVisitorEventArgs class.
        /// </summary>
        /// <param name="number">The save progress state.</param>
        public ProjectVisitorEventArgs(Int32 number)
        {
            _number = number;
            _message = String.Empty;
        }

        public ProjectVisitorEventArgs(String message)
            : this(0)
        {
            _message = message;
        }

        public ProjectVisitorEventArgs(String message, Exception visitorException)
            : this(0)
        {
            _message = message;
            _visitorException = visitorException;
        }

        public ProjectVisitorEventArgs(Exception visitorException)
            : this(0)
        {
            _message = visitorException.Message;
            _visitorException = visitorException;
        }

        public ProjectVisitorEventArgs(IProjectElement processedElement)
            :this(0)
        {
            _processedElement = processedElement;
        }
    }
}
