using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ProjectDoc.Model.Shapes
{
    internal class Shape2DRectangle : CompositeShape2D
    {
        #region Private Members

        /// <summary>
        /// The drawn rectangle.
        /// </summary>
        private Rectangle _rectLine;

        #endregion

        #region Constructors

        public Shape2DRectangle()
            : base()
        {
        }

        public Shape2DRectangle(IProjectElement owner)
            : base(owner)
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

            this.Size = new Size(60, 60);
            _rectLine = new Rectangle(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            _previousSize = this.Size;
        }

        protected override GraphicsPath GeneratePath()
        {
            GraphicsPath path = base.GeneratePath();

            _rectLine.X = this.Location.X;// + this.ScrollOffset.Width;
            _rectLine.Y = this.Location.Y; // + this.Size.Height;
            _rectLine.Width = this.Size.Width;
            _rectLine.Height = this.Size.Height;

            path.AddRectangle(_rectLine);

            return path;
        }

        //public override void Render(Graphics g)
        //{
        //    Font font = new Font(_font, _fontSize, FontStyle.Bold);
        //    StringFormat stringFormat = new StringFormat();
        //    stringFormat.Trimming = StringTrimming.EllipsisWord;
        //    stringFormat.Alignment = StringAlignment.Center;
        //    stringFormat.LineAlignment = StringAlignment.Center;

        //    //g.FillRectangle(Brushes.LightGray, _rectLine);
        //    //g.DrawString(this.Owner.Name, font, Brushes.Red, new Rectangle(_rectLine.X, _rectLine.Y, _rectLine.Width, _rectLine.Height / 2), stringFormat);
        //    //if (this.Owner is IDataInput<Int32>)
        //    //{
        //    //    g.DrawString(((IDataInput<Int32>)this.Owner).Data.ToString(), font, Brushes.Red,
        //    //        new Rectangle(_rectLine.X, _rectLine.Y + _rectLine.Height / 2, _rectLine.Width, _rectLine.Height / 2), stringFormat);
        //    //}
        //    //else if (this.Owner is IDataOutput<Int32>)
        //    //{
        //    //    g.DrawString(((IDataOutput<Int32>)this.Owner).Data.ToString(), font, Brushes.Red,
        //    //        new Rectangle(_rectLine.X, _rectLine.Y + _rectLine.Height / 2, _rectLine.Width, _rectLine.Height / 2), stringFormat);
        //    //}


        //    font.Dispose();
        //    stringFormat.Dispose();

        //    base.Render(g);
        //}

        #endregion
    }
}
