using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDoc.Model.Shapes
{
    internal class Shape2DOutput : Shape2DRectangle
    {
        #region Constructors

        public Shape2DOutput()
            : base()
        {
        }

        public Shape2DOutput(IProjectElement owner)
            : base(owner)
        {
        }

        #endregion
    }
}
