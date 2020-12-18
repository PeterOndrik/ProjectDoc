using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDoc.Model
{
    public static class ProjectElementExtension
    {
        public static void ForEach(this IProjectElement element, Action<IProjectElement> action)
        {
            foreach (IProjectElement e in element)
            {
                action(e);
            }
        }
    }
}
