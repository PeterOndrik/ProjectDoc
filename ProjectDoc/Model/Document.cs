using System;
using System.Collections.Generic;

using ProjectDoc.Operations;

namespace ProjectDoc.Model
{
    /// <summary>
    /// Represents a Document with configuration.
    /// </summary>
    internal class Document : CompositeProjectElement
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Document class.
        /// </summary>
        public Document()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs an operation over the document.
        /// </summary>
        /// <param name="visitor">The operation to perform.</param>
        public override void Accept(IProjectOperation visitor)
        {
            visitor.VisitDocument(this);
        }

        #endregion
    }
}
