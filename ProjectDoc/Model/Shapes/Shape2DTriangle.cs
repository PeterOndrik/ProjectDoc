using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDoc.Model.Shapes
{
    internal class Shape2DTriangle : CompositeShape2D
    {
        #region Constructors

        public Shape2DTriangle()
            : base()
        {
        }

        public Shape2DTriangle(IProjectElement owner)
            : base(owner)
        {
        }

        #endregion

        #region Shape2DMembers

        protected override void Initialize()
        {
            base.Initialize();

            this.Size = new Size(60, 60);
            _previousSize = this.Size;
        }

        protected override GraphicsPath GeneratePath()
        {
            GraphicsPath path = base.GeneratePath();

            Point p1 = this.Location;
            Point p2 = new Point(this.Location.X + this.Size.Width, this.Location.Y + this.Size.Height / 2);
            Point p3 = new Point(this.Location.X, this.Location.Y + this.Size.Height);

            path.AddPolygon(new Point[] { p1, p2, p3, p1 });

            return path;
        }

        //public override void Render(Graphics g)
        //{
        //    Font font = new Font(_font, _fontSize, FontStyle.Bold);
        //    StringFormat stringFormat = new StringFormat();
        //    stringFormat.Trimming = StringTrimming.EllipsisWord;
        //    stringFormat.Alignment = StringAlignment.Center;
        //    stringFormat.LineAlignment = StringAlignment.Center;

        //    Rectangle rect = this.GetLargestPossibleRegion();
        //    g.FillPath(Brushes.LightGray, this.Path);
        //    g.DrawString(this.Owner.Name, font, Brushes.Red, rect, stringFormat);

        //    font.Dispose();
        //    stringFormat.Dispose();

        //    base.Render(g);
        //}

        #endregion
    }
}
