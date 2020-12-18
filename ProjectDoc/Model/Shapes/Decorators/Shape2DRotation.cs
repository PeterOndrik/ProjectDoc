using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDoc.Model.Shapes.Decorators
{
    internal class Shape2DRotation : Shape2DDecorator
    {
        /// <summary>
        /// Gets or sets the angle in defrees of the clockwise rotation around the location point.
        /// </summary>
        public Single Rotation
        {
            get;
            set;
        }

        public Shape2DRotation(IShape2D shape)
            : base(shape)
        {
        }

        protected override GraphicsPath GeneratePath()
        {
            GraphicsPath path = base.GeneratePath();

            Matrix matrix = new Matrix();
            matrix.RotateAt(this.Rotation, this.Shape.Location);
            path.Transform(matrix);

            return path;
        }
    }
}
