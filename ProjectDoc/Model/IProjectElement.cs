using System;
using System.Collections.Generic;

using ProjectDoc.Model.Shapes;
using ProjectDoc.Model.Structure;
using ProjectDoc.Operations;

namespace ProjectDoc.Model
{
    /// <summary>
    /// Represents a generic collection of IProjectElement objects and custom controls.
    /// </summary>
    public interface IProjectElement : IList<IProjectElement>, IComparable<IProjectElement>
    {
        /// <summary>
        /// Gets a parent object.
        /// </summary>
        IProjectElement Parent
        {
            get;
        }

        /// <summary>
        /// Gets and sets the structure rules.
        /// </summary>
        IProjectStructure Structure
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of an IProjectElement instance.
        /// </summary>
        String Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the depth of a object in collection.
        /// </summary>
        Int32 Depth
        {
            get;
        }

        /// <summary>
        /// Gets or sets a graphics shape of an IProjectElement instance.
        /// </summary>
        IShape2D Shape
        {
            get;
            set;
        }

        /// <summary>
        /// Calls Visit operation for appropriate object. The Visitor Desing Pattern.
        /// </summary>
        /// <param name="visitor"></param>
        void Accept(IProjectOperation visitor);

        /// <summary>
        /// Return parent object of type T.
        /// </summary>
        /// <typeparam name="T">The type name of parent to return.</typeparam>
        /// <returns>The parent of type T.</returns>
        T GetParent<T>() where T : class;
    }
}
