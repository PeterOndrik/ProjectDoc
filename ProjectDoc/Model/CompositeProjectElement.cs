using System;
using System.Collections.Generic;

namespace ProjectDoc.Model
{
    /// <summary>
    /// Decorates a ProjectElement with generic collection.
    /// </summary>
    public abstract class CompositeProjectElement : ProjectElement
    {
        #region Protected Members

        /// <summary>
        /// The list of IProjectElement objects.
        /// </summary>
        protected IList<IProjectElement> _projectElements;

        #endregion

        #region Constructors

        public CompositeProjectElement()
        {
            _projectElements = new List<IProjectElement>();
        }

        #endregion

        #region Override Methods

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            foreach (ProjectElement pe in _projectElements)
            {
                pe.Dispose();
            }
        }

        #endregion

        #region IList<IProjectElement> Members

        public override int IndexOf(IProjectElement item)
        {
            return _projectElements.IndexOf(item);
        }

        public override void Insert(int index, IProjectElement item)
        {
             _projectElements.Insert(index, item);
            this.SetParent(item);
        }

        public override void RemoveAt(int index)
        {
            IProjectElement item = _projectElements[index];
            _projectElements.RemoveAt(index);
            this.ClearParent(item);
        }

        public override IProjectElement this[int index]
        {
            get
            {
                return _projectElements[index];
            }
            set
            {
                _projectElements[index] = value;
                this.SetParent(value);
            }
        }

        #endregion

        #region ICollection<IProjectElement> Members

        public override void Add(IProjectElement item)
        {
            _projectElements.Add(item);
            this.SetParent(item);
        }

        public override void Clear()
        {
            for (Int32 i = 0; i < _projectElements.Count; i++)
                this.ClearParent(_projectElements[i]);
            _projectElements.Clear();
        }

        public override bool Contains(IProjectElement projectElement)
        {
            return _projectElements.Contains(projectElement);
        }

        public override int Count
        {
            get
            {
                return _projectElements.Count;
            }
        }
        
        public override bool Remove(IProjectElement item)
        {
            bool result = _projectElements.Remove(item);
            if (result)
            {
                this.ClearParent(item);
            }

            return result;
        }

        #endregion

        #region IEnumerable<IProjectElement> Members

        public override IEnumerator<IProjectElement> GetEnumerator()
        {
            return _projectElements.GetEnumerator();
        }

        #endregion
    }
}
