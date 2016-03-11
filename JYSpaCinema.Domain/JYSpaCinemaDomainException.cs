using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JYSpaCinema
{
    public class JYSpaCinemaDomainException : Exception
    {
        public JYSpaCinemaDomainException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }
    }
}
