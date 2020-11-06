using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisioAddInTest
{
    public enum TermType
    {
        None,
        Start,
        Action,
        InterEventTimer,
        ExclusiveGateway,
        ParallelGateway,
        Rel,
        End,
        InclusiveGateway
    }
}
