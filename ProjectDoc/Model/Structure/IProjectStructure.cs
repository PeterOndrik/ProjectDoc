using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ProjectDoc.Model.Structure
{
    /// <summary>
    /// Defines methods to add a ProjectElement object into collection. 
    /// </summary>
    public interface IProjectStructure : IConvertible
    {
        /// <summary>
        /// Occurs when a warning is generated.
        /// </summary>
        event EventHandler<ProjectStructureEventArgs> Warning;

        /// <summary>
        /// Composes or recomposes all ProjectElement objects in collection according to the structure rules.
        /// </summary>
        /// <param name="projectElement">The ProjectElement object.</param>
        void Compose(IProjectElement projectElement);

        /// <summary>
        /// Add child ProjectElement object into parent according to the structure rules.
        /// </summary>
        /// <param name="parent">A parent ProjectElement object.</param>
        /// <param name="child">A child ProjectElement object.</param>
        void Compose(IProjectElement parent, IProjectElement child);

        /// <summary>
        /// Change value and make validity test.
        /// </summary>
        /// <param name="projectElement">A ProjectElement object to check validity.</param>
        /// <param name="propertyInfo">A property of ProjectElement object to check validity.</param>
        /// <param name="oldValue">The actual value.</param>
        /// <param name="newValue">The new value.</param>
        void ChangeValue(IProjectElement projectElement, PropertyInfo propertyInfo, Object oldValue, Object newValue, Boolean check);
    }
}
