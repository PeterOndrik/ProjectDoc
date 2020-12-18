using System;
using System.Collections.Generic;

using ProjectDoc.Operations;

namespace ProjectDoc.Model
{
    /// <summary>
    /// Represent the Project element.
    /// </summary>
    internal class Project : CompositeProjectElement
    {
        #region Properties

        /// <summary>
        /// Gets or sets the versio of a project.
        /// </summary>
        public String Version
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a new instance of the Project class.
        /// </summary>
        public Project()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs an operation over the project.
        /// </summary>
        /// <param name="visitor">An operation to perform.</param>
        public override void Accept(IProjectOperation visitor)
        {
            visitor.VisitProject(this);
        }

        #endregion
    }
}
