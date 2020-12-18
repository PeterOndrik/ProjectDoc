using ProjectDoc.Factories;
using ProjectDoc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDocTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IProjectElementFactory factory = new DefaultProjectElementFactory();
            IProjectElement project = factory.CreateProject(null, "Test");
            IProjectElement xmlFile = factory.CreateXmlFile(null, "test.xml", "e:\\test\\");
            project.Add(xmlFile);
            IProjectElement document = factory.CreateDocument(null);
            xmlFile.Add(document);
        }
    }
}
