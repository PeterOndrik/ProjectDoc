using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ProjectDoc.Model.Shapes
{
    /// <summary>
    /// Represents of a composition the shapes.
    /// </summary>
    internal abstract class CompositeShape2D : Shape2D
    {
        #region Protected Members

        /// <summary>
        /// The list of IShape2D objects.
        /// </summary>
        protected IList<IShape2D> _shapeList;
        protected Size _previousSize;

        #endregion

        #region Constructors

        public CompositeShape2D()
            :base()
        {

        }

        public CompositeShape2D(IProjectElement owner)
            : base(owner)
        {
        }

        #endregion

        #region Shape2D Members

        protected override void Initialize()
        {
            base.Initialize();

            _shapeList = new List<IShape2D>();
        }

        protected override GraphicsPath GeneratePath()
        {
            GraphicsPath path = base.GeneratePath();

            foreach (IShape2D shape in this)
            {
                int x = shape.Location.X;
                int y = shape.Location.Y;
                int xP = 1;
                if (shape.Location.X > 0)
                {
                    xP = _previousSize.Width / shape.Location.X;
                    if (xP == 0)
                        xP = 1;
                }
                int yP = 1;
                if (shape.Location.Y > 0)
                {
                    yP = _previousSize.Height / shape.Location.Y;
                    if (yP == 0)
                        yP = 1;
                }
                int shiftX = (this.Size.Width - _previousSize.Width) / xP;
                int shiftY = (this.Size.Height - _previousSize.Height) / yP;
                shape.Location = new Point(this.Location.X + x + shiftX, this.Location.Y + y + shiftY);
                if (shape.SuppressDraw == false)
                {
                    path.AddPath(shape.Path, true);
                }
                shape.Location = new Point(x + shiftX, y + shiftY);
            }
            //ProjectElement peParent = (ProjectElement)this.Owner.Parent;
            //while (peParent != null)
            //{
            //    if (peParent.Shape != null)
            //    {
            //        this.Location = peParent.Shape.Location;
            //        this.Size = peParent.Shape.Size;
            //        break;
            //    }
            //    peParent = (ProjectElement)peParent.Parent;
            //}

            _previousSize = this.Size;

            return path;
        }

        public override void Render(Graphics g)
        {
            base.Render(g);

            //foreach (IShape2D shape in this)
            //{
            //    if (g.ClipBounds.IntersectsWith(shape.GetLargestPossibleRegion()))
            //    {
            //        // subshapes have relative location
            //        int x = shape.Location.X;
            //        int y = shape.Location.Y;
            //        shape.Location = new Point(this.Location.X + x, this.Location.Y + y);
            //        shape.Render(g);
            //        shape.Location = new Point(x, y);
            //    }
            //}
        }

        #endregion

        #region IShape2D Members

        public override void Add(IShape2D item)
        {
            _shapeList.Add(item);
            Int32 x = this.Location.X + item.Location.X;
            Int32 y = this.Location.Y + item.Location.Y;
            item.Location = new Point(x, y);
        }

        public override IShape2D this[int index]
        {
            get
            {
                return _shapeList[index];
            }

            set
            {
                _shapeList[index] = value;
            }
        }

        public override IEnumerator<IShape2D> GetEnumerator()
        {
            return _shapeList.GetEnumerator();
        }

        #endregion
    }
}
