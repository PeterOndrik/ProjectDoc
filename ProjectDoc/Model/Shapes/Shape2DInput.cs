using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDoc.Model.Shapes
{
    internal class Shape2DInput : Shape2DRectangle
    {
        #region Constructors

        public Shape2DInput()
            : base()
        {
        }

        public Shape2DInput(IProjectElement owner)
            : base(owner)
        {
        }

        #endregion
    }
}
