using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Common
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException(string message) : base(message)
        {
            
        }
    }
}
