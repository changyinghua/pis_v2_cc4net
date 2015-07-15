using CCApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis.cc4net.Handlers
{
    internal class CCSetPositionHandler : CCMessageHandler
    {
        internal override void Run(CCApiNet.CCInterface cci, ICCSubsystem subsystem, CCApiNet.IBaseMessage msg)
        {
            SetPosition position = (SetPosition)msg;
            subsystem.ChangePosition(Convert.ToInt32(position.WinXPos),Convert.ToInt32(position.WinYPos));
        }
    }
}
