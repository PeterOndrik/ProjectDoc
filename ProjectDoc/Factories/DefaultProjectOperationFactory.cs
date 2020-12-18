using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDoc.Operations;

namespace ProjectDoc.Factories
{
    public class DefaultProjectOperationFactory : IProjectOperationFactory
    {
        public IProjectOperation CreateInterpreting()
        {
            return new Interpreting();
        }
    }
}
