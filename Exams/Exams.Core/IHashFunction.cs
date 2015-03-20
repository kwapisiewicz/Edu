using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.Core
{
    public interface IHashFunction
    {
        string Evaluate(string secureString);
    }
}
