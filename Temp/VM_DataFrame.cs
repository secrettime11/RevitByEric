using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp
{
    public class VM_DataFrame
    {
        public M_DataFrame M_DataFrame;

        public M_DataFrame ObjImg 
        {
            get { return M_DataFrame; }
            set { M_DataFrame = value; }
        }
    }
}
