using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDoc.Operations;

namespace ProjectDoc.Model
{
    internal class Input<T> : CompositeProjectElement, IDataInput<T>
    {
        public T Data
        {
            get;
            set;
        }

        public override void Accept(IProjectOperation visitor)
        {
            visitor.VisitInput(this);
        }
    }
}
