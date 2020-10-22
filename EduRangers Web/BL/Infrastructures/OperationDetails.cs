using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Infrastructures
{
    public class OperationDetails
    {
        public OperationDetails(bool succedeed, string message)
        {
            this.Succedeed = succedeed;
            this.Message = message;
        }
        public bool Succedeed { get; private set; }
        public string Message { get; private set; }
    }
}
