using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDoc.Model.Shapes.Decorators
{
    internal abstract class Shape2DDecorator : Shape2D
    {
        public IShape2D Shape
        {
            get;
            set;
        }

        public Shape2DDecorator(IShape2D shape)
            : base(shape.Owner)
        {
            this.Shape = shape;
            this.Location = this.Shape.Location;
            this.Size = this.Shape.Size;
        }

        protected override GraphicsPath GeneratePath()
        {
            this.Shape.Location = this.Location;    // only to dispose the decorated shape
            this.Shape.Size = this.Size;
            GraphicsPath path = this.Shape.Path;

            return path;
        }

        #region IList<IProjectElement> Member

        public override IShape2D this[int index]
        {
            get
            {
                return this.Shape;
            }

            set
            {
                this.Shape = value;
            }
        }

        #endregion
    }
}
