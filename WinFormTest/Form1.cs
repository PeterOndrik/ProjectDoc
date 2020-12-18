using ProjectDoc.Factories;
using ProjectDoc.Model;
using ProjectDoc.Operations;
using ProjectDoc.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            IProjectElementFactory elFactory = new DefaultProjectElementFactory();

            IProjectElement project = elFactory.CreateProject(null, "TestProj");
            IProjectElement xmlFile = elFactory.CreateXmlFile(null, "test.xml", "e:\\test\\");
            project.Add(xmlFile);
            IProjectElement document = elFactory.CreateDocument(null, "TestDoc");
            xmlFile.Add(document);
            IProjectElement input1 = elFactory.CreateInput<Int32>(null, "Input1");
            input1.Shape.Location = new Point(20, 20);
            ((IDataInput<Int32>)input1).Data = 1;
            document.Add(input1);
            IProjectElement input2 = elFactory.CreateInput<Int32>(null, "Input2");
            input2.Shape.Location = new Point(20, 120);
            ((IDataInput<Int32>)input2).Data = 2;
            document.Add(input2);
            IProjectElement addition1 = elFactory.CreateOperator(null, "Addition1");
            addition1.Shape.Location = new Point(120, 80);
            document.Add(addition1);
            IProjectElement input3 = elFactory.CreateInput<Int32>(null, "Input3");
            input3.Shape.Location = new Point(20, 220);
            ((IDataInput<Int32>)input3).Data = 3;
            document.Add(input3);
            IProjectElement addition2 = elFactory.CreateOperator(null, "Addition2");
            addition2.Shape.Location = new Point(220, 120);
            document.Add(addition2);
            IProjectElement output = elFactory.CreateOutput<Int32>(null, "Result");
            output.Shape.Location = new Point(320, 140);
            document.Add(output);

            IProjectElement connection = elFactory.CreateConnection(null, input1, addition1, "Conn1");
            connection = elFactory.CreateConnection(null, input2, addition1, "Conn2");
            connection = elFactory.CreateConnection(null, addition1, addition2, "Conn3");
            connection = elFactory.CreateConnection(null, input3, addition2, "Conn4");
            connection = elFactory.CreateConnection(null, addition2, output, "Conn5");

            IProjectOperationFactory opFactory = new DefaultProjectOperationFactory();

            IProjectOperation interpret = opFactory.CreateInterpreting();

            project.Accept(interpret);

            ProjectDrawingView pd = new ProjectDrawingView();
            pd.Dock = DockStyle.Fill;
            pd.Add(project);

            this.Controls.Add(pd);
        }
    }
}
