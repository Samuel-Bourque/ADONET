using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET_Services
{
    public class PCoursException : Exception
    {
        public PCoursException(string message) : base(message) { }
    }
}
