using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ProjectDoc.Model.Shapes
{
    /// <summary>
    /// Defines methods and properties for all 2D shape.
    /// </summary>
    public interface IShape2D : IList<IShape2D>, IComparable<IShape2D>
    {
        /// <summary>
        /// Gets or sets the ambient foreground color of a shape.
        /// </summary>
        Color ForeColor { get; set; }

        /// <summary>
        /// Gets or sets the ambient background color of a shape.
        /// </summary>
        Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of the shape relative to the upper-left corner of its container.
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Gets or sets the height and width of a shape. 
        /// </summary>
        Size Size { get; set; }

        /// <summary>
        /// Gets the current GraphicsPath that represents the shape.
        /// </summary>
        GraphicsPath Path { get; }

        /// <summary>
        /// Gets or sets the track of an shape.
        /// </summary>
        Boolean Selected { get; set; }

        /// <summary>
        /// A number that represents the layer on which a control is placed. The 0 layer is on the top.
        /// </summary>
        Int32 ZOrder { get; set; }

        /// <summary>
        /// The parent of this shape.
        /// </summary>
        IShape2D Parent { get; }

        /// <summary>
        /// If true, the object will not be drawn but bound objects will be drawn.
        /// </summary>
        Boolean SuppressDraw { get; set; }

        /// <summary>
        /// Gets or sets the drag mode. If true the shape is in drag mode, otherwise false.
        /// </summary>
        Boolean IsDragging { get; set; }

        /// <summary>
        /// Gets or sets the resize mode. If true the shape is in resize mode, otherwise false.
        /// </summary>
        Boolean IsResizing { get; set; }

        /// <summary>
        /// Gets or sets the size of the scrolling.
        /// </summary>
        Size ScrollOffset { get; set; }

        /// <summary>
        /// The owner of this shape.
        /// </summary>
        IProjectElement Owner { get; }

        /// <summary>
        /// Returns the region occupied by the border and focus rectangle. This is useful when refreshing the form. 
        /// </summary>
        /// <returns></returns>
        Rectangle GetLargestPossibleRegion();

        /// <summary>
        /// The drawing logic. Each descendant can to have own drawing logic.
        /// </summary>
        /// <param name="g">The GDI+ drawing surface.</param>
        void Render(Graphics g);

        /// <summary>
        /// Refreshes the current 2D shape.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Check if the point is in the shape.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        Boolean HitTest(Point point);

        /// <summary>
        /// Check if the point is in the outline of the shape.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        Boolean HitTestBorder(Point point);

        /// <summary>
        /// You can perform a simple test for the focus border by hit-testing two rectangles—the outer
        /// rectangle (where the focus border is drawn) and the inner rectangle (where the control is
        /// drawn). If the point falls inside the outer rectangle but not inside the inner rectangle, the focus
        /// border was hit.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="hitSpot"></param>
        /// <returns></returns>
        Boolean HitTestFocusBorder(Point point, out HitSpot hitSpot);
    }
}
