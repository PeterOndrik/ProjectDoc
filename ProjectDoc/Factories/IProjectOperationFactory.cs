using ProjectDoc.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDoc.Factories
{
    public interface IProjectOperationFactory
    {
        IProjectOperation CreateInterpreting();
    }
}
