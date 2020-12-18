using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDoc.Model.Structure
{
    public class ProjectStructureEventArgs : EventArgs
    {
        public String Message
        {
            get;
            private set;
        }

        public Exception StructureException
        {
            get;
            private set;
        }

        public ProjectStructureEventArgs(String message)
        {
            this.Message = message;
        }

        public ProjectStructureEventArgs(Exception structureException)
        {
            this.StructureException = structureException;
        }
    }
}
