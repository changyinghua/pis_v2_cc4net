using CCApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pis.cc4net.Handlers
{
    internal class HeartbeatHandler : CCMessageHandler
    {
        internal override void Run(CCInterface cci, ICCSubsystem subsystem, CCApiNet.IBaseMessage msg)
        {
            Heartbeat hb = (Heartbeat)msg;
            int counter = Int32.Parse(hb.HeartbeatCount);
            subsystem.HealthCheck(counter);

            counter++;
            if(counter > 9999)
            {
                counter = 1;
            }
            String value = "000" + counter;
            hb.HeartbeatCount = value.Substring(value.Length - 4);
            cci.SendMessage(hb);
        }
    }
}
