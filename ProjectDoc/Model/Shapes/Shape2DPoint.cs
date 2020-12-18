using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDoc.Model.Shapes
{
    /// <summary>
    /// Represents a connection point.
    /// </summary>
    internal abstract class Shape2DPoint : Shape2D
    {
        #region Constructors

        public Shape2DPoint()
            :base()
        {
        }

        public Shape2DPoint(IProjectElement owner)
            :base(owner)
        {
        }

        #endregion

        #region Shape2D Members

        /// <summary>
        /// Initialize the base properties.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            this.Size = new Size(7, 7);
        }

        #endregion
    }
}
