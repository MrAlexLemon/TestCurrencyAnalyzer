using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Exceptions
{
    public abstract class AppException : Exception
    {
        public virtual string Code { get; }

        protected AppException(string message) : base(message)
        {
        }
    }
}
