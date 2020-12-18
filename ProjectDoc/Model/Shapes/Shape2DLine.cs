using ProjectDoc.Model.Shapes.Decorators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ProjectDoc.Model.Shapes
{
    /// <summary>
    /// Represents a connection shape between two IShape2D objects. The shape consists from line and description.
    /// </summary>
    internal class Shape2DLine : CompositeShape2D
    {
        #region Constructors

        public Shape2DLine(IProjectElement owner) 
            : base(owner)
        {
        }

        #endregion

        #region Shape2D Members

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override GraphicsPath GeneratePath()
        {
            //GraphicsPath path =  base.GeneratePath();
            GraphicsPath path = new GraphicsPath();

            // line has two elements (from, to)
            // if one of them has banned drawing, don't draw line
            if (this.Owner[0].Shape.SuppressDraw || this.Owner[1].Shape.SuppressDraw)
                return path;

            // calculation the nearest point on another object
            Point from = new Point();
            Point to = new Point();
            for (Int32 i = 0; i < 2; i++)
            {
                IProjectElement element = this.Owner[i];
                Double max = Double.MaxValue;
                Double distance = Double.MinValue;
                if (i == 0)
                {
                    IProjectElement element2 = this.Owner[i + 1];
                    // the middle of an element
                    to = new Point(element2.Shape.Location.X + element2.Shape.Size.Width / 2, element2.Shape.Location.Y + element2.Shape.Size.Height / 2);
                }
                else
                {
                    to = from;
                }

                Point searchPoint = new Point();
                foreach (IShape2D curPointShape in element.Shape)
                {
                    IShape2D pointShape = curPointShape;
                    while(pointShape is Shape2DDecorator)
                    {
                        pointShape = pointShape[0];
                    }
                    if (pointShape is Shape2DPoint)
                    {
                        int x = curPointShape.Location.X + element.Shape.Location.X;
                        int y = curPointShape.Location.Y + element.Shape.Location.Y;

                        distance = Math.Sqrt(Math.Pow(x - to.X, 2) + Math.Pow(y - to.Y, 2));
                        if (distance < max)
                        {
                            searchPoint = new Point(x, y);
                            max = distance;
                        }
                    }
                }

                if (i == 0)
                {
                    from = searchPoint;
                }
                else
                {
                    to = searchPoint;
                }
            }
            //path.AddLine(from, to);

            this.Location = from;
            this.Size = new Size(to.X - from.X, to.Y - from.Y);

            int yy = 0;
            foreach (IShape2D shape in this)
            {
                if (shape.SuppressDraw == false)
                {
                    if (yy == 0)
                    {
                       shape.Location = new Point(from.X, from.Y);
                       from = new Point(from.X + shape.Size.Width, from.Y);
                    }
                    else
                    {
                        shape.Location = new Point(to.X, to.Y);
                        to = new Point(to.X - shape.Size.Width, to.Y);
                    }
                    path.AddPath(shape.Path, true);
                }
                yy++;
            }
            path.AddLine(from, to);

            return path;
        }

        #endregion
    }
}
