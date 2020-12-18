using ProjectDoc.Operations;

namespace ProjectDoc.Model
{
    /// <summary>
    /// Represents a connection line between an IProjectElement source object and an IProjectElement destination object.
    /// </summary>
    internal class Connection : ProjectElement
    {
        #region Private Members

        IProjectElement[] _elementList = { null, null };

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Connection class.
        /// </summary>
        public Connection()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs an operation over a Connection instance.
        /// </summary>
        /// <param name="visitor">An operation to perform.</param>
        public override void Accept(IProjectOperation visitor)
        {
            visitor.VisitConnection(this);
        }

        #endregion

        #region IList<IProjectElement> Member

        public override IProjectElement this[int index]
        {
            get
            {
                return _elementList[index];
            }

            set
            {
                _elementList[index] = value;
            }
        }

        #endregion
    }
}
