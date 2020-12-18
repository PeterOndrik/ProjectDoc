using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDoc.Model.Shapes
{
    internal class Shape2DInputPoint : Shape2DPoint
    {
        #region Constructors

        public Shape2DInputPoint()
            :base()
        {
        }

        public Shape2DInputPoint(IProjectElement owner)
            :base(owner)
        {
        }

        #endregion

        #region Shape2D Members

        protected override GraphicsPath GeneratePath()
        {
            GraphicsPath path = base.GeneratePath();

            Point p1 = this.Location;
            Point p2 = new Point(this.Location.X, this.Location.Y - this.Size.Height / 2);
            Point p3 = new Point(this.Location.X + this.Size.Width, this.Location.Y);
            Point p4 = new Point(this.Location.X, this.Location.Y + this.Size.Height / 2);

            path.AddPolygon(new Point[] { p1, p2, p3, p4, p1 });

            return path;
        }

        #endregion
    }
}
