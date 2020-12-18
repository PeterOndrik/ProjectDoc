using ProjectDoc.Model;
using ProjectDoc.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDoc.Factories
{
    public interface IShape2DFactory
    {
        IShape2D CreateShape2DEmpty();
        IShape2D CreateShape2DEmpty(IProjectElement owner);
        IShape2D CreateShape2DLine();
        IShape2D CreateShape2DLine(IProjectElement owner);
        IShape2D CreateShape2DInputPoint();
        IShape2D CreateShape2DInputPoint(IProjectElement owner);
        IShape2D CreateShape2DOutputPoint();
        IShape2D CreateShape2DOutputPoint(IProjectElement owner);
        IShape2D CreateShape2DRectangle();
        IShape2D CreateShape2DRectangle(IProjectElement owner);
        IShape2D CreateShape2DTriangle();
        IShape2D CreateShape2DTriangle(IProjectElement owner);
        IShape2D CreateShape2DInput();
        IShape2D CreateShape2DInput(IProjectElement owner);
        IShape2D CreateShape2DOutput();
        IShape2D CreateShape2DOutput(IProjectElement owner);
    }
}
