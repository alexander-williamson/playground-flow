using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flow.Library
{
    public interface IDataManipulation
    {
        IDictionary<string, object> Manipulate(IDictionary<string, object> variables, string key);
    }
}
