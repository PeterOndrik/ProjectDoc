using ProjectDoc.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDoc
{
    public class ProjectService
    {
        private static IProjectElementFactory _factory = new DefaultProjectElementFactory();

        public static IProjectElementFactory Factory
        {
            get
            {
                return _factory;
            }
        }
    }
}
