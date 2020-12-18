using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDoc.Model.Shapes
{
    /// <summary>
    /// The shape needs the ability to hit-test arbitrary points, and see if they fall inside the bounds of the path.
    /// There are three types of hit test:
    /// - Checking if a point falls inside a shape.
    /// - Checking if a point falls on the edge of a shape.
    /// - Checking if a point falls on the focus cue (dotted rectangle) drawn around a shape.
    /// </summary>
    public enum HitSpot
    {
        Top,
        Bottom,
        Left,
        Right,
        TopLeftCorner,
        BottomLeftCorner,
        TopRightCorner,
        BottomRightCorner,
        None
    }
}
