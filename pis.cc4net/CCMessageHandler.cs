using CCApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pis.cc4net
{
    internal abstract class CCMessageHandler
    {
        internal abstract void Run(CCInterface cci, ICCSubsystem subsystem, IBaseMessage msg);
    }
}
