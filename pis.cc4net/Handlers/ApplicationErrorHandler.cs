using CCApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis.cc4net.Handlers
{
    internal class ApplicationErrorHandler : CCMessageHandler
    {
        internal override void Run(CCApiNet.CCInterface cci, ICCSubsystem subsystem, CCApiNet.IBaseMessage msg)
        {
            ApplicationError error = (ApplicationError)msg;
            subsystem.Error(error.ErrorMsgType, error.ErrorMessage);
        }
    }
}
