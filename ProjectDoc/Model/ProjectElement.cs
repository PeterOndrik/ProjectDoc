using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Reflection;

using ProjectDoc.Model.Shapes;
using ProjectDoc.Model.Structure;
using ProjectDoc.Model.Exceptions;
using ProjectDoc.Operations;
using System.Linq;

namespace ProjectDoc.Model
{
    /// <summary>
    /// Represent an abstract class for each IProjectElement in a collection. 
    /// </summary>
    /// <remarks>
    /// IProjectElement is based on Composite desing pattern.
    /// </remarks>
    public abstract class ProjectElement : IProjectElement, IDisposable
    {
        #region Private Members

        /// <summary>
        /// The reference to the parent IProjectElement object.
        /// </summary>
        private IProjectElement _parent;
        /// <summary>
        /// The referece to the structure of data.
        /// </summary>
        private IProjectStructure _structure;
        /// <summary>
        /// The IProjectElement name in the TreeView component.
        /// </summary>
        private String _name;
        /// <summary>
        /// Holds the reference to graphics presentation of the IProjectElement object.
        /// </summary>
        private IShape2D _shape;
        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private Boolean _disposed;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of ProjectElement.
        /// </summary>
        public virtual String Name
        {
            set
            {
                _name = value;
            }
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets or sets the image for the IProjectElement object.
        /// </summary>
        public Bitmap ImageNode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the visibility of the IProjectElement object.
        /// </summary>
        public Boolean NodeVisibility
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the behaviour for the executing common visitor. If true (default), will be not used IProjectElement (common) visitor.
        /// </summary>
        public Boolean OwnVisitor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the IProjectElement will be expand. If true, the ProjectElement will be expand, otherwise false.
        /// </summary>
        public Boolean Expand
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets a graphics shape IShape2D of a ProjectElement object.
        /// </summary>
        public virtual IShape2D Shape
        {
            get
            {
                return _shape;
            }
            set
            {
                _shape = value;
            }
        }

        public virtual Int32 X
        {
            get
            {
                if (_shape != null)
                    return _shape.Location.X;
                return 0;
            }
            set
            {
                if (_shape != null)
                    _shape.Location = new Point(value, _shape.Location.Y);
            }
        }

        public virtual Int32 Y
        {
            get
            {
                if (_shape != null)
                    return _shape.Location.Y;
                return 0;
            }
            set
            {
                if (_shape != null)
                    _shape.Location = new Point(_shape.Location.X, value);
            }
        }

        public virtual Int32 Width
        {
            get
            {
                if (_shape != null)
                    return _shape.Size.Width;
                return 0;
            }
            set
            {
                if (_shape != null)
                    _shape.Size = new Size(value, _shape.Size.Height);
            }
        }

        public virtual Int32 Height
        {
            get
            {
                if (_shape != null)
                    return _shape.Size.Height;
                return 0;
            }
            set
            {
                if (_shape != null)
                    _shape.Size = new Size(_shape.Size.Width, value);
            }
        }

        #endregion
        
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ProjectElement class.
        /// </summary>
        public ProjectElement()
        {
            this._name = String.Empty;
            this._disposed = false;
            this.Shape = new Shape2DEmpty(this);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Releases all resources used by the ActivationContext. 
        /// </summary>
        /// <param name="disposing">If true dispose managed resources.</param>
        /// <remarks>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// 1. If disposing equals true, the method has been called directly or indirectly by a user's code. 
        ///    Managed and unmanaged resources can be disposed.
        /// 2. If disposing equals false, the method has been called by the runtime from inside the finalizer and you should not reference
        ///    other objects. Only unmanaged resources can be disposed.
        /// </remarks>
        protected void Dispose(Boolean disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this._disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    this.DisposeManagedResources();
                }
                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.
                this.DisposeUnmanagedResources();
            }
            this._disposed = true;
        }

        /// <summary>
        /// Dispose managed resources.
        /// </summary>
        protected virtual void DisposeManagedResources()
        {
        }

        /// <summary>
        /// Dispose unmanaged resources.
        /// </summary>
        protected virtual void DisposeUnmanagedResources()
        {
            if (this.ImageNode != null)
            {
                this.ImageNode.Dispose();
                this.ImageNode = null;
            }
        }

        protected void SetParent(IProjectElement item)
        {
            ((ProjectElement)item).Parent = this;
            item.Shape.ZOrder = this.Shape.ZOrder + 1;
        }

        protected void ClearParent(IProjectElement item)
        {
            ((ProjectElement)item).Parent = null;
        }

        #endregion

        #region IProjectElement Members

        public virtual IProjectElement Parent
        {
            get
            {
                return _parent;
            }
            private set
            {
                _parent = value;
            }
        }

        public virtual IProjectStructure Structure
        {
            get
            {
                return _structure;
            }
            set
            {
                _structure = value;
            }
        }

        public virtual int Depth
        {
            get
            {
                Int32 depth = 0;
                for (IProjectElement pe = this.Parent; pe != null; pe = pe.Parent)
                {
                    depth++;
                }
                return depth;
            }
        }

        public T GetParent<T>() where T : class
        {
            T parent = null;
            IProjectElement pe = this;
            while (pe.Parent != null)
            {
                pe = pe.Parent;
                if (pe is T)
                {
                    parent = (T)pe;
                    break;
                }

            }
            return parent;
        }

        public ProjectElement GetParent(Type type)
        {
            ProjectElement parent = null;
            ProjectElement pe = this;
            while (pe.Parent != null)
            {
                pe = (ProjectElement)pe.Parent;
                if (pe.GetType() == type)
                {
                    parent = pe;
                    break;
                }
            }
            return parent;
        }

        public virtual void Accept(IProjectOperation visitor)
        {
            throw new ProjectElementNotImplementedException(this, MethodBase.GetCurrentMethod());
        }

        #endregion
        
        #region IList<IProjectElement> Members

        public virtual int IndexOf(IProjectElement item)
        {
            throw new ProjectElementNotImplementedException(this, MethodBase.GetCurrentMethod());
        }

        public virtual void Insert(int index, IProjectElement item)
        {
            throw new ProjectElementNotImplementedException(this, MethodBase.GetCurrentMethod());
        }

        public virtual void RemoveAt(int index)
        {
            throw new ProjectElementNotImplementedException(this, MethodBase.GetCurrentMethod());
        }

        public virtual IProjectElement this[int index]
        {
            get
            {
                throw new ProjectElementNotImplementedException(this, MethodBase.GetCurrentMethod());
            }
            set
            {
                throw new ProjectElementNotImplementedException(this, MethodBase.GetCurrentMethod());
            }
        }

        #endregion

        #region ICollection<IProjectElement> Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(IProjectElement item)
        {
            throw new ProjectElementNotImplementedException(this, MethodBase.GetCurrentMethod());
        }

        public virtual void Clear()
        {
        }

        public virtual bool Contains(IProjectElement item)
        {
            return false;
        }

        public virtual void CopyTo(IProjectElement[] array, int arrayIndex)
        {
            throw new ProjectElementNotImplementedException(this, MethodBase.GetCurrentMethod());
        }

        public virtual Int32 Count
        {
            get
            {
                return 0;
            }
        }

        public virtual bool IsReadOnly
        {
            get 
            {
                return false;
            }
        }

        public virtual bool Remove(IProjectElement item)
        {
            throw new ProjectElementNotImplementedException(this, MethodBase.GetCurrentMethod());
        }

        #endregion

        #region IEnumerable<IProjectElement> Members

        public virtual IEnumerator<IProjectElement> GetEnumerator()
        {
            return Enumerable.Empty<IProjectElement>().GetEnumerator(); // yield break;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.Empty<IProjectElement>().GetEnumerator(); // yield break;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to take this object off the finalization queue
            // and prevent finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IComparable<IProjectElement> Members

        public int CompareTo(IProjectElement pe)
        {
            throw new ProjectElementNotImplementedException(this, MethodBase.GetCurrentMethod());
        }

        #endregion

        #region Destructor

        ~ProjectElement()
        {
            this.Dispose(false);
        }

        #endregion
    }
}
