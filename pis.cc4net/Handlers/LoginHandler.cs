using CCApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pis.cc4net.Handlers
{
    internal class LoginHandler : CCMessageHandler
    {
        internal override void Run(CCInterface cci, ICCSubsystem subsystem, CCApiNet.IBaseMessage msg)
        {
            LogIn login = (LogIn) msg;

            bool isEnglish = login.Language.Equals("01") ? true : false;

            subsystem.Login(login.UserID, isEnglish, login.StaffGrade);
        }
    }
}
