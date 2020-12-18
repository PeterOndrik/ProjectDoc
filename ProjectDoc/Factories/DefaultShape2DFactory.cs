using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDoc.Model;
using ProjectDoc.Model.Shapes;
using System.Drawing;
using System.Drawing.Drawing2D;
using ProjectDoc.Model.Shapes.Decorators;

namespace ProjectDoc.Factories
{
    public class DefaultShape2DFactory : IShape2DFactory
    {
        public IShape2D CreateShape2DEmpty()
        {
            return new Shape2DEmpty();
        }

        public IShape2D CreateShape2DEmpty(IProjectElement owner)
        {
            return new Shape2DEmpty(owner);
        }

        public IShape2D CreateShape2DLine()
        {
            IShape2D line =  this.CreateShape2DLine(null);

            return line;
        }

        public IShape2D CreateShape2DLine(IProjectElement owner)
        {
            Shape2DLine line = new Shape2DLine(owner);
            IShape2D point = this.CreateShape2DInputPoint(owner);
            point.Location = new Point(line.Location.X, line.Location.Y);
            point.SuppressDraw = true;
            line.Add(point);
            point = this.CreateShape2DOutputPoint(owner);
            point.Location = new Point(line.Location.X + line.Size.Width, line.Location.Y + line.Size.Height);
            line.Add(point);
      
            return line;
        }

        public IShape2D CreateShape2DInputPoint()
        {
            IShape2D point = new Shape2DInputPoint();

            return point;
        }

        public IShape2D CreateShape2DInputPoint(IProjectElement owner)
        {
            IShape2D point = new Shape2DInputPoint(owner);

            return point;

        }
        public IShape2D CreateShape2DOutputPoint()
        {
            IShape2D point = new Shape2DOutputPoint();

            return point;
        }

        public IShape2D CreateShape2DOutputPoint(IProjectElement owner)
        {
            IShape2D point = new Shape2DOutputPoint(owner);

            return point;
        }

        public IShape2D CreateShape2DRectangle()
        {
            IShape2D rectangle = this.CreateShape2DRectangle(null);

            return rectangle;
        }

        public IShape2D CreateShape2DRectangle(IProjectElement owner)
        {
            Shape2DRectangle rectangle = new Shape2DRectangle(owner);

            return rectangle;
        }

        public IShape2D CreateShape2DTriangle()
        {
            IShape2D rectangle = this.CreateShape2DTriangle(null);

            return rectangle;
        }

        public IShape2D CreateShape2DTriangle(IProjectElement owner)
        {
            Shape2DTriangle triangle = new Shape2DTriangle(owner);
            IShape2D point = this.CreateShape2DInputPoint(owner);
            point.Location = new Point(triangle.Location.X, triangle.Location.Y + triangle.Size.Height / 4);
            point.SuppressDraw = true;
            triangle.Add(point);
            point = this.CreateShape2DInputPoint(owner);
            point.Location = new Point(triangle.Location.X, triangle.Location.Y + triangle.Size.Height / 4 * 3);
            point.SuppressDraw = true;
            triangle.Add(point);
            point = this.CreateShape2DOutputPoint(owner);
            point.Location = new Point(triangle.Location.X + triangle.Size.Width, triangle.Location.Y + triangle.Size.Height / 2);
            point.SuppressDraw = true;
            triangle.Add(point);

            return triangle;
        }

        public IShape2D CreateShape2DInput()
        {
            IShape2D input = this.CreateShape2DInput(null);

            return input;
        }

        public IShape2D CreateShape2DInput(IProjectElement owner)
        {
            Shape2DInput input = new Shape2DInput(owner);
            IShape2D point = this.CreateShape2DOutputPoint(owner);
            Shape2DRotation rotatedPoint = new Shape2DRotation(point);
            rotatedPoint.Location = new Point(input.Location.X + input.Size.Width / 2, input.Location.Y);
            rotatedPoint.Rotation = -90;
            rotatedPoint.SuppressDraw = true;
            input.Add(rotatedPoint);
            point = this.CreateShape2DOutputPoint(owner);
            rotatedPoint = new Shape2DRotation(point);
            rotatedPoint.Location = new Point(input.Location.X + input.Size.Width, input.Location.Y + input.Size.Height / 2);
            rotatedPoint.Rotation = 0;
            rotatedPoint.SuppressDraw = true;
            input.Add(rotatedPoint);
            point = this.CreateShape2DOutputPoint(owner);
            rotatedPoint = new Shape2DRotation(point);
            rotatedPoint.Location = new Point(input.Location.X + input.Size.Width / 2, input.Location.Y + input.Size.Height);
            rotatedPoint.Rotation = 90;
            rotatedPoint.SuppressDraw = true;
            input.Add(rotatedPoint);
            point = this.CreateShape2DOutputPoint(owner);
            rotatedPoint = new Shape2DRotation(point);
            rotatedPoint.Location = new Point(input.Location.X, input.Location.Y + input.Size.Height / 2);
            rotatedPoint.Rotation = 180;
            rotatedPoint.SuppressDraw = true;
            input.Add(rotatedPoint);

            return input;
        }

        public IShape2D CreateShape2DOutput()
        {
            IShape2D input = this.CreateShape2DOutput(null);

            return input;
        }

        public IShape2D CreateShape2DOutput(IProjectElement owner)
        {
            Shape2DOutput output = new Shape2DOutput(owner);
            IShape2D point = this.CreateShape2DInputPoint(owner);
            Shape2DRotation rotatedPoint = new Shape2DRotation(point);
            rotatedPoint.Location = new Point(output.Location.X + output.Size.Width / 2, output.Location.Y);
            rotatedPoint.Rotation = 90;
            rotatedPoint.SuppressDraw = true;
            output.Add(rotatedPoint);
            point = this.CreateShape2DInputPoint(owner);
            rotatedPoint = new Shape2DRotation(point);
            rotatedPoint.Location = new Point(output.Location.X + output.Size.Width, output.Location.Y + output.Size.Height / 2);
            rotatedPoint.Rotation = 180;
            rotatedPoint.SuppressDraw = true;
            output.Add(rotatedPoint);
            point = this.CreateShape2DInputPoint(owner);
            rotatedPoint = new Shape2DRotation(point);
            rotatedPoint.Location = new Point(output.Location.X + output.Size.Width / 2, output.Location.Y + output.Size.Height);
            rotatedPoint.Rotation = -90;
            rotatedPoint.SuppressDraw = true;
            output.Add(rotatedPoint);
            point = this.CreateShape2DInputPoint(owner);
            rotatedPoint = new Shape2DRotation(point);
            rotatedPoint.Location = new Point(output.Location.X, output.Location.Y + output.Size.Height / 2);
            rotatedPoint.Rotation = 0;
            rotatedPoint.SuppressDraw = true;
            output.Add(rotatedPoint);

            return output;
        }
    }
}
