using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Core.Exceptions
{
    public class ApprovalRequiredException: Exception
    {
        public ApprovalRequiredException() { }
        public ApprovalRequiredException(string errorMessage): base(errorMessage) { }
    }
}
