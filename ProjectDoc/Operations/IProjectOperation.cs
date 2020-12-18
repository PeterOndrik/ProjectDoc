using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ProjectDoc.Model;

namespace ProjectDoc.Operations
{
    /// <summary>
    /// Declares an operation for each class in the object structure. The operation's name and signature identifies the class that sends the request to the visitor.
    /// That lets the visitor determine the concrete class of the element being visited. Then the visitor can access the element directly through its particular interface.
    /// </summary>
    public interface IProjectOperation : IDisposable
    {
        #region Events

        /// <summary>
        /// Occurs when an operation was started.
        /// </summary>
        event EventHandler<ProjectVisitorEventArgs> Started;

        /// <summary>
        /// Occurs when an operation changes its progress.
        /// </summary>
        event EventHandler<ProjectVisitorEventArgs> ProgressChanged;

        /// <summary>
        /// Occurs when an operation was finished.
        /// </summary>
        event EventHandler<ProjectVisitorEventArgs> Finished;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the description name.
        /// </summary>
        String Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or set the description image.
        /// </summary>
        Image ImageNode
        {
            get;
            set;
        }

        #endregion

        #region Operational Methods

        /// <summary>
        /// Performs an operation on a XmlFile instance.
        /// </summary>
        /// <param name="xmlFile">The XmlFile instance on which an operation will be performed.</param>
        void VisitXmlFile(IProjectElement xmlFile);

        /// <summary>
        /// Performs an operation on a Project instance.
        /// </summary>
        /// <param name="project">The Project instance on which an operation will be performed.</param>
        void VisitProject(IProjectElement project);

        /// <summary>
        /// Performs an operation on a Document instance.
        /// </summary>
        /// <param name="document">The Document instance on which an operation will be performed.</param>
        void VisitDocument(IProjectElement document);

        /// <summary>
        /// Performs an operation on a Connection instance. 
        /// </summary>
        /// <param name="connection">The Connection instance on which an operation will be performed.</param>
        void VisitConnection(IProjectElement connection);

        void VisitOperator(IProjectElement operatorx);
        void VisitInput(IProjectElement input);
        void VisitOutput(IProjectElement output);

        #endregion
    }
}
