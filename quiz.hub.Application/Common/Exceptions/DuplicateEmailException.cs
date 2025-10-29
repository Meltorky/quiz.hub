using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Common.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string message)  : base(message) { }
    }
}
