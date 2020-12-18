using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Collections;
using System.Linq;

namespace ProjectDoc.Model.Shapes
{
    /// <summary>
    /// Represents abstract class from which other types of shapes derive.
    /// </summary>
    internal abstract class Shape2D : IShape2D
    {
        #region Private/Protected Static Members

        /// <summary>
        /// The distance for selected border.
        /// </summary>
        private static Int32 _focusBorderSpace = 5;
        /// <summary>
        /// The central font family.
        /// </summary>
        protected static String _font = "Tahoma";
        /// <summary>
        /// The central font size.
        /// </summary>
        protected static Single _fontSize = 7f;
        /// <summary>
        /// The central font style.
        /// </summary>
        protected static Int32 _fontStyle = (Int32)FontStyle.Regular;

        #endregion

        #region Private Members

        /// <summary>
        /// The ambient foreground color of an shape.
        /// </summary>
        private Color _foreColor;

        /// <summary>
        /// The ambient background color of an shape.
        /// </summary>
        private Color _backColor;

        /// <summary>
        /// The coordinates of the upper-left corner of the shape relative to the upper-left corner of its container.
        /// </summary>
        private Point _location;

        /// <summary>
        /// The height and width of an shape.
        /// </summary>
        private Size _size;

        /// <summary>
        /// Even internally, all access to the path should go through the Path property, so that the path is regenerated if null.
        /// </summary>
        private GraphicsPath _path;

        /// <summary>
        /// One of the major new features in the Shape class is the ability to draw a focus rectangle. 
        /// </summary>
        private Boolean _selected;

        /// <summary>
        /// The numeric z-index value.
        /// Shapes have built-in support for layering. You can use methods like BringToFront() and SendToBack() to change how controls overlap.
        /// </summary>
        private Int32 _zOrder;

        /// <summary>
        /// If true, the shape is in drag mode, otherwise false.
        /// </summary>
        private Boolean _isDragging;

        /// <summary>
        /// If true, the shape is in resize mode, otherwise false.
        /// </summary>
        private Boolean _isResizing;

        /// <summary>
        /// The offset if the surface to draw was scrolled.
        /// </summary>
        private Size _scrollOffset;

        /// <summary>
        /// A project element for which belongs this shape.
        /// </summary>
        private IProjectElement _owner;

        /// <summary>
        /// If true, the object will not be drawn but bound objects will be drawn.
        /// </summary>
        private Boolean _suppressDraw;

        /// <summary>
        /// The central Pen object.
        /// </summary>
        private Pen _outlinePen;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the Shape2D class.
        /// </summary>
        public Shape2D()
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the Shape2D class with a specified project element owner of the shape.
        /// </summary>
        /// <param name="owner">The owner of the shape.</param>
        public Shape2D(IProjectElement owner)
        {
            _owner = owner;
            this.Initialize();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Initialize the base properties.
        /// </summary>
        protected virtual void Initialize()
        {
            _foreColor = Color.Black;
            _outlinePen = new Pen(_foreColor, 1);
            _zOrder = 0;
        }

        /// <summary>
        /// Creates the corresponding GraphicsPath for the shape.
        /// </summary>
        protected virtual void RefreshPath()
        {
            _path = this.GeneratePath();
        }

        /// <summary>
        /// Generates the shape. The task to the deriving class, through an abstract GeneratePath() method.
        /// </summary>
        /// <returns>The shape GraphicsPath.</returns>
        protected virtual GraphicsPath GeneratePath()
        {
            return new GraphicsPath();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the string of hexadecimal reprezentation of the shape.
        /// </summary>
        /// <returns>The hexadecimal reprezentation string.</returns>
        public override string ToString()
        {
            Stream stream = null;
            StringBuilder outputData = new StringBuilder();
            try
            {
                stream = new MemoryStream();
                BinaryFormatter f = new BinaryFormatter();
                f.Serialize(stream, this);
                stream.Position = 0;

                Byte[] data = new Byte[1024];
                Int32 dataCount = 0;
                while ((dataCount = stream.Read(data, 0, 1024)) > 0)
                {
                    for (Int32 i = 0; i < dataCount; i++)
                    {
                        outputData.Append(data[i].ToString("X2"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return outputData.ToString();
        }

        #endregion

        #region Destructor

        ~Shape2D()
        {
            if (_outlinePen != null)
                _outlinePen.Dispose();
            if (_path != null)
                _path.Dispose();
        }

        #endregion

        #region IShape2D Members

        /// <summary>
        /// Gets or sets the ambient foreground color of a shape.
        /// </summary>
        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                _foreColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the ambient background color of a shape.
        /// </summary>
        public Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of the shape relative to the upper-left corner of its container.
        /// </summary>
        public Point Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                // it is needed to create new GraphicsPath, because the position was changed, draw the new shape
                if (_path != null)
                {
                    _path.Dispose();
                    _path = null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the height and width of a shape.
        /// </summary>
        public Size Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                // it is needed to create new GraphicsPath, because the size was changed, draw the new shape
                if (_path != null)
                {
                    _path.Dispose();
                    _path = null;
                }
            }
        }

        /// <summary>
        /// Gets the current GraphicsPath that represents the shape.
        /// </summary>
        public GraphicsPath Path
        {
            get
            {
                // The path is refreshed automatically as needed.
                if (_path == null)
                    this.RefreshPath();

                return _path;
            }
        }

        /// <summary>
        /// Gets or sets the track of an shape.
        /// </summary>
        public Boolean Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

        /// <summary>
        /// A number that represents the layer on which a control is placed. The 0 layer is on the top.
        /// </summary>
        public int ZOrder
        {
            get
            {
                return _zOrder;
            }
            set
            {
                _zOrder = value;
            }
        }

        /// <summary>
        /// Gets or sets the parent shape.
        /// </summary>
        public IShape2D Parent
        {
            get
            {
                IProjectElement pe = this.Owner;
                if (pe.Parent == null)
                    return null;
                while (pe.Parent != null)
                {
                    pe = pe.Parent;
                    if (pe.Shape != null)
                        break;
                }
                return pe.Shape;
            }
        }

        /// <summary>
        /// If true, the object will not be drawn but bound objects will be drawn.
        /// </summary>
        public Boolean SuppressDraw
        {
            get
            {
                return _suppressDraw;
            }
            set
            {
                _suppressDraw = value;
                if (_suppressDraw)
                {
                    if (_path != null)
                    {
                        _path.Dispose();
                        _path = null;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the drag mode.
        /// </summary>
        public virtual Boolean IsDragging
        {
            get
            {
                return _isDragging;
            }
            set
            {
                _isDragging = value;
            }
        }

        /// <summary>
        /// Gets or sets the resize mode.
        /// </summary>
        public virtual Boolean IsResizing
        {
            get
            {
                return _isResizing;
            }
            set
            {
                _isResizing = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the scrolling.
        /// </summary>
        public Size ScrollOffset
        {
            get
            {
                return _scrollOffset;
            }
            set
            {
                _scrollOffset = value;
                //this.Location = new Point(this.Location.X, this.Location.Y);
                if (_path != null)
                {
                    _path.Dispose();
                    _path = null;
                }
            }
        }

        /// <summary>
        /// Gest a ProjectElement for which belongs this shape.
        /// </summary>
        public IProjectElement Owner
        {
            get
            {
                return _owner;
            }
        }

        /// <summary>
        /// Returns the region occupied by the border and focus rectangle. This is useful when refreshing the form.
        /// This method returns a Region object that represents the maximum space that the shape can occupy, which occurs when the focus rectangle is drawn.
        /// </summary>
        /// <returns></returns>
        public virtual Rectangle GetLargestPossibleRegion()
        {
            if (this.Path == null)
                return Rectangle.Empty;

            Rectangle rect = Rectangle.Round(this.Path.GetBounds());
            rect.Inflate(new Size(_focusBorderSpace, _focusBorderSpace));

            return rect;
        }

        /// <summary>
        /// Centralized the painting logic.
        /// </summary>
        /// <param name="g"></param>
        public virtual void Render(Graphics g)
        {
            g.DrawPath(_outlinePen, this.Path);

            //Font font = new Font(_font, _fontSize, FontStyle.Bold);
            //StringFormat stringFormat = new StringFormat();
            //stringFormat.Trimming = StringTrimming.EllipsisWord;
            //stringFormat.Alignment = StringAlignment.Center;
            //stringFormat.LineAlignment = StringAlignment.Center;

            //Rectangle nameRectangle = this.GetLargestPossibleRegion();
            //g.DrawString(this.Owner.Name, font, Brushes.Red, nameRectangle, stringFormat);

            //font.Dispose();
            //stringFormat.Dispose();

            // if required, paint the focus box
            if (this.Selected)
            {
                Rectangle rect = this.GetLargestPossibleRegion();
                ControlPaint.DrawFocusRectangle(g, rect);
            }
        }

        /// <summary>
        /// Rehreshes the current 2D shape.
        /// </summary>
        public virtual void Refresh()
        {
            this.Location = new Point(this.Location.X, this.Location.Y);

            foreach (IProjectElement pe in this.Owner)
            {
                pe.Shape.Refresh();
            }
        }

        /// <summary>
        /// Check if the point is in the shape.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Boolean HitTest(Point point)
        {
            if (this.Path == null)
                return false;

            return Path.IsVisible(point);
        }

        /// <summary>
        /// Check if the point is in the outline of the shape.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Boolean HitTestBorder(Point point)
        {
            if (_outlinePen == null)
            {
                return false;
            }
            else
            {
                if (this.Path == null)
                    return false;

                return Path.IsOutlineVisible(point, _outlinePen);
            }
        }

        /// <summary>
        /// You can perform a simple test for the focus border by hit-testing two rectangles — the outer
        /// rectangle (where the focus border is drawn) and the inner rectangle (where the control is
        /// drawn). If the point falls inside the outer rectangle but not inside the inner rectangle, the focus
        /// border was hit.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="hitSpot"></param>
        /// <returns></returns>
        public bool HitTestFocusBorder(Point point, out HitSpot hitSpot)
        {
            hitSpot = HitSpot.None;
            // Ignore controls that don't have a focus square.
            if (!this.Selected)
            {
                return false;
            }
            else
            {
                if (this.Path == null)
                    return false;

                Rectangle rectInner = Rectangle.Round(Path.GetBounds());
                Rectangle rectOuter = rectInner;
                rectOuter.Inflate(new Size(_focusBorderSpace, _focusBorderSpace));
                if (rectOuter.Contains(point) && !rectInner.Contains(point))
                {
                    // Point is on (or close enough) to the focus square.
                }
                else
                {
                    return false;
                }

                // Unfortunately, the Rectangle.Contains() method can't give you any information about where the hit occured.
                // To get these details, you need to go extra work of comparing the space between the clicked point and 
                // the appropriate edge. You need to perform all these tests for every point, in case it's close to two edges,
                // in which case it's interpreted as a corner hit.
                bool top = false;
                bool bottom = false;
                bool left = false;
                bool right = false;
                // Check the point against all edges.
                if (Math.Abs(point.X - this.Location.X - this.ScrollOffset.Width) < _focusBorderSpace)
                    left = true;
                if (Math.Abs(point.X - (this.Location.X + this.ScrollOffset.Width + this.Size.Width)) < _focusBorderSpace)
                    right = true;
                if (Math.Abs(point.Y - this.Location.Y - this.ScrollOffset.Height) < _focusBorderSpace)
                    top = true;
                if (Math.Abs(point.Y - (this.Location.Y + this.ScrollOffset.Height + this.Size.Height)) < _focusBorderSpace)
                    bottom = true;
                // Determine the hit spot based on the edges that are close.
                if (top && left) 
                    hitSpot = HitSpot.TopLeftCorner;
                else if (top && right) 
                    hitSpot = HitSpot.TopRightCorner;
                else if (bottom && left) 
                    hitSpot = HitSpot.BottomLeftCorner;
                else if (bottom && right) 
                    hitSpot = HitSpot.BottomRightCorner;
                else if (top) 
                    hitSpot = HitSpot.Top;
                else if (bottom) 
                    hitSpot = HitSpot.Bottom;
                else if (left) 
                    hitSpot = HitSpot.Left;
                else if (right) 
                    hitSpot = HitSpot.Right;
                if (hitSpot == HitSpot.None)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion

        #region IList<IShape2D> Members

        public int IndexOf(IShape2D item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IShape2D item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public virtual void Add(IShape2D item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IShape2D item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IShape2D[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IShape2D item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get
            {
                return 0;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual IShape2D this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual IEnumerator<IShape2D> GetEnumerator()
        {
            return Enumerable.Empty<IShape2D>().GetEnumerator(); // yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.Empty<IShape2D>().GetEnumerator(); // yield break;
        }

        #endregion

        #region IComparable<IShape2D> Members

        public int CompareTo(IShape2D shape)
        {
            return this.ZOrder.CompareTo(shape.ZOrder);
        }

        #endregion
    }
}
