using CCApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pis.cc4net.Handlers
{
    internal class CCSetViewHandler : CCMessageHandler
    {
        internal override void Run(CCInterface cci, ICCSubsystem subsystem, CCApiNet.IBaseMessage msg)
        {
            subsystem.MoveToTop();
        }
    }
}
