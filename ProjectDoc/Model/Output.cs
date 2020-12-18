using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDoc.Operations;

namespace ProjectDoc.Model
{
    internal class Output<T> : CompositeProjectElement, IDataOutput<T>
    {
        public T Data
        {
            get;
            private set;
        }

        public override void Accept(IProjectOperation visitor)
        {
            visitor.VisitOutput(this);
        }
    }
}
