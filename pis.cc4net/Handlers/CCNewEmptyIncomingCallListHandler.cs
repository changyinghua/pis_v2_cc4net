using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis.cc4net.Handlers
{
    internal class CCNewEmptyIncomingCallListHandler : CCMessageHandler
    {
        internal override void Run(CCApiNet.CCInterface cci, ICCSubsystem subsystem, CCApiNet.IBaseMessage msg)
        {
            subsystem.NewEmptyIncomingCallList();
        }
    }
}
