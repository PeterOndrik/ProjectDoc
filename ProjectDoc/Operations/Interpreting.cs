using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDoc.Model;
using ProjectDoc.Operations;
using System.Diagnostics;

namespace ProjectDoc.Operations
{
    internal class Interpreting : ProjectOperation
    {
        public override void VisitOperator(IProjectElement operatorx)
        {
            Debug.Assert(operatorx.Count > 0);


            //operatorx.ForEach()
        }
    }
}
