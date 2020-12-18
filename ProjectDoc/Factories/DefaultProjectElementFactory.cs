using ProjectDoc.Model;
using ProjectDoc.Model.Shapes;
using ProjectDoc.Model.Structure;

using System;
using System.Drawing;

namespace ProjectDoc.Factories
{
    /// <summary>
    /// Represents a default factory to create the IProjectElement instances.
    /// </summary>
    public class DefaultProjectElementFactory : IProjectElementFactory
    {
        #region Private Members

        IShape2DFactory _shape2DFactory = new DefaultShape2DFactory();

        #endregion

        #region IProjectFactory Members

        public virtual IProjectElement CreateEmptyElement()
        {
            EmptyElement emptyElement = new EmptyElement();
            emptyElement.Shape = _shape2DFactory.CreateShape2DEmpty(emptyElement);

            return emptyElement;
        }

        public virtual IProjectElement CreateProject(IProjectStructure structure)
        {
            Project project = new Project();
            project.Structure = structure;
            project.Expand = true;

            return project;
        }

        public virtual IProjectElement CreateProject(IProjectStructure structure, String name)
        {
            IProjectElement project = this.CreateProject(structure);
            project.Name = name;

            return project;
        }

        public virtual IProjectElement CreateXmlFile(IProjectStructure structure)
        {
            XmlFile xmlFile = new XmlFile();
            xmlFile.Structure = structure;
            xmlFile.Expand = true;

            return xmlFile;
        }

        public virtual IProjectElement CreateXmlFile(IProjectStructure structure, String fileName, String directory)
        {
            XmlFile xmlFile = (XmlFile)this.CreateXmlFile(structure);
            xmlFile.Name = fileName;
            xmlFile.Directory = directory;

            return xmlFile;
        }

        public virtual IProjectElement CreateDocument(IProjectStructure structure)
        {
            Document document = new Document();
            document.Structure = structure;
            document.Expand = true;

            return document;
        }

        public virtual IProjectElement CreateDocument(IProjectStructure structure, String name)
        {
            IProjectElement document = this.CreateDocument(structure);
            document.Name = name;

            return document;
        }

        public virtual IProjectElement CreateConnection(IProjectStructure structure, IProjectElement element1, IProjectElement element2)
        {
            Connection connection = new Connection();
            connection.Structure = structure;
            connection.Shape = _shape2DFactory.CreateShape2DLine(connection);
            element1.Add(connection);
            element2.Add(connection);
            connection[0] = element1;
            connection[1] = element2;

            return connection;
        }

        public virtual IProjectElement CreateConnection(IProjectStructure structure, IProjectElement element1, IProjectElement element2,  String name)
        {
            Connection connection = (Connection)this.CreateConnection(structure, element1, element2);
            connection.Name = name;

            return connection;
        }

        public virtual IProjectElement CreateInput<T>(IProjectStructure structure)
        {
            Input<T> input = new Input<T>();
            input.Structure = structure;
            input.Shape = _shape2DFactory.CreateShape2DInput(input);

            return input;
        }

        public virtual IProjectElement CreateInput<T>(IProjectStructure structure, String name)
        {
            IProjectElement input = this.CreateInput<T>(structure);
            input.Name = name;

            return input;
        }

        public virtual IProjectElement CreateOperator(IProjectStructure structure)
        {
            Operator operator1 = new Operator();
            operator1.Structure = structure;
            operator1.Shape = _shape2DFactory.CreateShape2DTriangle(operator1);

            return operator1;
        }

        public virtual IProjectElement CreateOperator(IProjectStructure structure, String name)
        {
            IProjectElement operator1 = this.CreateOperator(structure);
            operator1.Name = name;

            return operator1;
        }

        public virtual IProjectElement CreateOutput<T>(IProjectStructure structure)
        {
            Output<T> output = new Output<T>();
            output.Structure = structure;
            output.Shape = _shape2DFactory.CreateShape2DOutput(output);

            return output;
        }

        public virtual IProjectElement CreateOutput<T>(IProjectStructure structure, String name)
        {
            IProjectElement output = this.CreateOutput<T>(structure);
            output.Name = name;

            return output;
        }

        #endregion
    }
}
