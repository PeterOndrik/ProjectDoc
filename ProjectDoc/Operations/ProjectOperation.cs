using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDoc.Model;

namespace ProjectDoc.Operations
{
    internal abstract class ProjectOperation : IProjectOperation
    {
        public Image ImageNode
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public event EventHandler<ProjectVisitorEventArgs> Finished;
        public event EventHandler<ProjectVisitorEventArgs> ProgressChanged;
        public event EventHandler<ProjectVisitorEventArgs> Started;

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual void VisitConnection(IProjectElement connection)
        {
            connection.ForEach(e => e.Accept(this));
        }

        public virtual void VisitDocument(IProjectElement document)
        {
            document.ForEach(e => e.Accept(this));
        }

        public virtual void VisitInput(IProjectElement input)
        {
            input.ForEach(e => e.Accept(this));
        }

        public virtual void VisitOperator(IProjectElement operatorx)
        {
            operatorx.ForEach(e => e.Accept(this));
        }

        public virtual void VisitOutput(IProjectElement output)
        {
            output.ForEach(e => e.Accept(this));
        }

        public virtual void VisitProject(IProjectElement project)
        {
            project.ForEach(e => e.Accept(this));
        }

        public virtual void VisitXmlFile(IProjectElement xmlFile)
        {
            xmlFile.ForEach(e => e.Accept(this));
        }
    }
}
