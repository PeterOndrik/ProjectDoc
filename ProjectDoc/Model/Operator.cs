using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDoc.Operations;

namespace ProjectDoc.Model
{
    internal class Operator : CompositeProjectElement
    {
        public override void Accept(IProjectOperation visitor)
        {
            visitor.VisitOperator(this);
        }
    }
}
