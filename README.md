# ProjectDoc
The ProjectDoc is a library to provide functionality for drawing process flow diagrams.
## Example:
![ProjectDoc Diagram Example](ProjectDocExample.PNG "Figure 1: ProjectDoc Diagram Example")
```c#
IProjectElementFactory elFactory = new DefaultProjectElementFactory();

IProjectElement project = elFactory.CreateProject(null, "Example");
IProjectElement xmlFile = elFactory.CreateXmlFile(null, "example.xml", ".\\");
project.Add(xmlFile);
IProjectElement document = elFactory.CreateDocument(null, "ExampleDoc");
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

ProjectDrawingView pd = new ProjectDrawingView();
pd.Dock = DockStyle.Fill;
pd.Add(project);

this.Controls.Add(pd);
```