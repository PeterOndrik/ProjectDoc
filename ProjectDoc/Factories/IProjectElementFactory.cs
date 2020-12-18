using ProjectDoc.Model;
using ProjectDoc.Model.Structure;
using System;

namespace ProjectDoc.Factories
{
    public interface IProjectElementFactory
    {
        IProjectElement CreateEmptyElement();
        IProjectElement CreateXmlFile(IProjectStructure structure);
        IProjectElement CreateXmlFile(IProjectStructure structure, String fileName, String directory);
        IProjectElement CreateProject(IProjectStructure structure);
        IProjectElement CreateProject(IProjectStructure structure, String name);
        IProjectElement CreateDocument(IProjectStructure structure);
        IProjectElement CreateDocument(IProjectStructure structure, String name);
        IProjectElement CreateConnection(IProjectStructure structure, IProjectElement source, IProjectElement destination);
        IProjectElement CreateConnection(IProjectStructure structure, IProjectElement source, IProjectElement destination, String name);
        IProjectElement CreateInput<T>(IProjectStructure structure);
        IProjectElement CreateInput<T>(IProjectStructure structure, String name);
        IProjectElement CreateOperator(IProjectStructure structure);
        IProjectElement CreateOperator(IProjectStructure structure, String name);
        IProjectElement CreateOutput<T>(IProjectStructure structure);
        IProjectElement CreateOutput<T>(IProjectStructure structure, String name);
    }
}
