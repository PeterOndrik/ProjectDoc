using ProjectDoc.Model.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ProjectDoc.Model.Structure;
using ProjectDoc.Operations;
using System.Collections;
using ProjectDoc.Factories;
using ProjectDoc.Model;
using System.Drawing.Drawing2D;

namespace ProjectDoc.View
{
    public partial class ProjectDrawingView : Panel, IProjectElement
    {
        private static IProjectElementFactory _factory = ProjectService.Factory;

        #region Private Members

        /// <summary>
        /// The list of IProjectElement objects.
        /// </summary>
        private List<IProjectElement> _projectElements = new List<IProjectElement>();

        /// <summary>
        /// Track the currently selected shape.
        /// </summary>
        private IShape2D _currentShape = _factory.CreateEmptyElement().Shape;

        /// <summary>
        /// The X shape position in the custom window.
        /// </summary>
        private Int32 _clickOffsetX;

        /// <summary>
        /// The Y shape position in the custom window.
        /// </summary>
        private Int32 _clickOffsetY;

        /// <summary>
        /// The exact spot where the hit occurs.
        /// </summary>
        private HitSpot _resizingMode;

        #endregion

        #region Constructor

        public ProjectDrawingView()
        {
            InitializeComponent();

            this.BackColor = Color.White;
            this.DoubleBuffered = true;

            this.AutoScroll = true;
            this.HScroll = true;
            this.VScroll = true;
            this.AutoScrollMinSize = new Size(500, 500);
        }

        public ProjectDrawingView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// Loops through all the shapes and calls their HitTest() and HitTestBorder() methods, looking for a hit. 
        /// The important part of this method is that before it starts checking, it sorts the collection so that the lowest
        /// z-index elements are first. This ensures that if one image is layered on top of another, the image
        /// on top has the first chance to receive the mouse click.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public IShape2D HitTest(Point point)
        {
            //IComparer<IProjectElement> zorder = new ZOrderComparer();
            //_projectElements.Sort(zorder);
            foreach (ProjectElement pe in _projectElements)
            {
                if (pe.Shape == null)
                    continue;
                if (pe.Shape.HitTest(point) || pe.Shape.HitTestBorder(point))
                    return pe.Shape;
            }
            return _factory.CreateEmptyElement().Shape;
        }

        #region Private Methods

        private void ProjectDrawing_MouseDown(object sender, MouseEventArgs e)
        {
            // check for a hit on a focus square
            HitSpot hitSpot;
            if (_currentShape.Selected && _currentShape.HitTestFocusBorder(new Point(e.X, e.Y), out hitSpot))
            {
                // turn on resize mode, the border was clicked
                _clickOffsetX = e.X - _currentShape.Location.X;
                _clickOffsetY = e.Y - _currentShape.Location.Y;
                _currentShape.IsResizing = true;
            }
            else
            {
                IShape2D shape = this.HitTest(new Point(e.X, e.Y));
                _currentShape.Selected = false;
                this.Invalidate(_currentShape.GetLargestPossibleRegion());
                _currentShape = shape;
                _currentShape.Selected = true;
                this.Invalidate(_currentShape.GetLargestPossibleRegion());
                _clickOffsetX = e.X - _currentShape.Location.X;
                _clickOffsetY = e.Y - _currentShape.Location.Y;
                _currentShape.Location = new Point(e.X - _clickOffsetX, e.Y - _clickOffsetY);
                if (e.Button == MouseButtons.Left)
                {
                    // start dragging mode
                    _currentShape.IsDragging = true;
                    Cursor = Cursors.SizeAll;
                }
            }
        }

        private void ProjectDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            if (_currentShape.IsDragging)
            {
                if (e.X - _clickOffsetX > 0 && e.Y - _clickOffsetY > 0)
                {
                    _currentShape.Location = new Point(e.X - _clickOffsetX, e.Y - _clickOffsetY);
                    _currentShape.Refresh();
                }
                // repaint whole surface
                this.Invalidate();
            }
            else if (_currentShape.IsResizing)
            {
                int minSize = 5;

                // keep track of the old size, useful for invalidating when NOT double-buffering
                Rectangle oldPosition = _currentShape.GetLargestPossibleRegion();
                // resize a shape according to the resize mode
                switch (_resizingMode)
                {
                    case HitSpot.Top:
                        this.HitSpotTopHandle(e, minSize);
                        break;
                    case HitSpot.Bottom:
                        this.HitSpotBottomHandle(e, minSize);
                        break;
                    case HitSpot.Left:
                        this.HitSpotLeftHandle(e, minSize);
                        break;
                    case HitSpot.Right:
                        this.HitSpotRightHandle(e, minSize);
                        break;
                    case HitSpot.TopLeftCorner:
                        this.HitSpotTopHandle(e, minSize);
                        this.HitSpotLeftHandle(e, minSize);
                        break;
                    case HitSpot.TopRightCorner:
                        this.HitSpotTopHandle(e, minSize);
                        this.HitSpotRightHandle(e, minSize);
                        break;
                    case HitSpot.BottomLeftCorner:
                        this.HitSpotBottomHandle(e, minSize);
                        this.HitSpotLeftHandle(e, minSize);
                        break;
                    case HitSpot.BottomRightCorner:
                        this.HitSpotBottomHandle(e, minSize);
                        this.HitSpotRightHandle(e, minSize);
                        break;
                }
                // find the largest invalidate area for NO double buffer scenario
                Rectangle newPosition = _currentShape.GetLargestPossibleRegion();
                _currentShape.Refresh();
                //this.Invalidate(Rectangle.Union(oldPosition, newPosition));
                this.Invalidate();
            }
            else
            {
                if (_currentShape.Selected && _currentShape.HitTestFocusBorder(new Point(e.X, e.Y), out _resizingMode))
                {
                    switch (_resizingMode)
                    {
                        case HitSpot.Top:
                        case HitSpot.Bottom:
                            this.Cursor = Cursors.SizeNS;
                            break;
                        case HitSpot.Left:
                        case HitSpot.Right:
                            this.Cursor = Cursors.SizeWE;
                            break;
                        case HitSpot.TopLeftCorner:
                        case HitSpot.BottomRightCorner:
                            this.Cursor = Cursors.SizeNWSE;
                            break;
                        case HitSpot.BottomLeftCorner:
                        case HitSpot.TopRightCorner:
                            this.Cursor = Cursors.SizeNESW;
                            break;
                        default:
                            this.Cursor = Cursors.Default;
                            break;
                    }
                }
                else if (_currentShape.Selected && _currentShape.HitTest(new Point(e.X, e.Y)) == true)
                {
                    this.Cursor = Cursors.SizeAll;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void ProjectDrawing_MouseUp(object sender, MouseEventArgs e)
        {
            _currentShape.IsDragging = false;
            _currentShape.IsResizing = false;
            this.Cursor = Cursors.Default;
        }

        private void HitSpotTopHandle(MouseEventArgs e, Int32 minSize)
        {
            if (e.Y < _currentShape.Location.Y + _currentShape.Size.Height - minSize)
            {
                _currentShape.Size = new Size(_currentShape.Size.Width, _currentShape.Location.Y + _currentShape.Size.Height - (e.Y - _clickOffsetY));
                _currentShape.Location = new Point(_currentShape.Location.X, e.Y - _clickOffsetY);
            }
        }

        private void HitSpotBottomHandle(MouseEventArgs e, Int32 minSize)
        {
            if (e.Y - _currentShape.ScrollOffset.Height > _currentShape.Location.Y + minSize)
            {
                _currentShape.Size = new Size(_currentShape.Size.Width, e.Y - _currentShape.Location.Y - _currentShape.ScrollOffset.Height);
            }
        }

        private void HitSpotLeftHandle(MouseEventArgs e, Int32 minSize)
        {
            if (e.X < _currentShape.Location.X + _currentShape.Size.Width - minSize)
            {
                _currentShape.Size = new Size((_currentShape.Location.X + _currentShape.Size.Width) - (e.X - _clickOffsetX), _currentShape.Size.Height);
                _currentShape.Location = new Point(e.X - _clickOffsetX, _currentShape.Location.Y);
            }
        }

        private void HitSpotRightHandle(MouseEventArgs e, Int32 minSize)
        {
            if (e.X - _currentShape.ScrollOffset.Width > _currentShape.Location.X + minSize)
            {
                _currentShape.Size = new Size(e.X - _currentShape.Location.X - _currentShape.ScrollOffset.Width, _currentShape.Size.Height);
            }
        }

        #endregion

        #region Protected Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            foreach (ProjectElement pe in _projectElements)
            {
                // Get the invalidated region from the PaintEventArgs.ClipRectangle property. The region is DrawingSurfacePresent window size.
                // Now is overlaped only the invalidated region with a given shape.
                // The all shapes visible in the window will be redrawn.
                if (e.ClipRectangle.IntersectsWith(pe.Shape.GetLargestPossibleRegion()))
                {
                    pe.Shape.Render(e.Graphics);
                }
            }
            base.OnPaint(e);
        }

        #endregion

        #region IProjectElement Members

        public IProjectElement this[int index]
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

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Depth
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IShape2D Shape
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

        public IProjectStructure Structure
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

        IProjectElement IProjectElement.Parent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Accept(IProjectOperation visitor)
        {
            throw new NotImplementedException();
        }

        public void Add(IProjectElement item)
        {
            _projectElements.Add(item);
            foreach (IProjectElement pe in item)
            {
                this.Add(pe);
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IProjectElement other)
        {
            throw new NotImplementedException();
        }

        public bool Contains(IProjectElement item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IProjectElement[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IProjectElement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T GetParent<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public int IndexOf(IProjectElement item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IProjectElement item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IProjectElement item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
