using CCApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis.cc4net.Handlers
{
    internal class CCSetDimensionHandler : CCMessageHandler
    {
        internal override void Run(CCApiNet.CCInterface cci, ICCSubsystem subsystem, CCApiNet.IBaseMessage msg)
        {
            SetDimension s = (SetDimension)msg;

            int width = Convert.ToInt32(s.Width);

            if(width > 1920)
            {
                width=1920;
            }

            int height = Convert.ToInt32(s.Height);

            if(height>1012)
            {
                height = 1012;
            }

            subsystem.ChangeDimension(width, height);
        }
    }
}
