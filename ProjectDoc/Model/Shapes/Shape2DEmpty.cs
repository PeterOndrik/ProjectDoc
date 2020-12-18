using ProjectDoc.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDoc.Model.Shapes
{
    internal class Shape2DEmpty : Shape2D
    {
        #region Constructors

        public Shape2DEmpty()
            : base()
        {
        }

        public Shape2DEmpty(IProjectElement owner)
            : base(owner)
        {
        }

        #endregion

        #region Shape2D Members

        protected override void Initialize()
        {
            base.Initialize();

            this.SuppressDraw = true;
        }

        #endregion
    }
}
