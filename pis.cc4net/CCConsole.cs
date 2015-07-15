using CCApiNet;
using pis.cc4net.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis.cc4net
{
    public class CCConsole
    {
        private ICCSubsystem subsystem;

        private CCInterface cci;

        private Dictionary<String, CCMessageHandler> handlers;

        public CCConsole()
        {
            this.cci = new CCInterface();
            this.cci.Setup(Subsystem.PIS);
            this.cci.MessageReceived += OnMessageReceived;

            InitializeHandlers();
        }

        public CCResultCode Connect()
        {
            return (CCResultCode)this.cci.Connect();
        }

        public CCResultCode Disconnect()
        {
            return (CCResultCode)this.cci.Disconnect();
        }

        public void Config(ICCSubsystem subsystem)
        {
            this.subsystem = subsystem;
        }

        public CCResultCode Startup()
        {
            return (CCResultCode)this.cci.SendMessage(new StartUp());
        }

        public CCResultCode NewIncomingCall(string timeStamp, string callId, string aliasName, bool isIncomingCall, bool isHighPriority)
        {
            if(this.cci==null)
            {
                return CCResultCode.CCERR_UNKNOWN_FAILURE;
            }

            NewIncomingCallReq req = new NewIncomingCallReq();
            req.MsgSeq = "    ";
            req.NumOfSubMessages="0001";

            MemoryStream stream = new MemoryStream();
            stream.Write(Encoding.ASCII.GetBytes("    "), 0, 4);
            stream.Write(Encoding.ASCII.GetBytes("0001"), 0, 4);

            byte[] p1 =  Encoding.ASCII.GetBytes(timeStamp.ToString(14));       //14
            stream.Write(p1, 0, p1.Length);

            byte[] p2 = Encoding.ASCII.GetBytes(callId.ToString(16));           //16
            stream.Write(p2, 0, p2.Length);

            byte[] p3 = Encoding.Unicode.GetBytes(aliasName.ToString(20));      //40
            stream.Write(p3, 0, p3.Length);



            byte[] p4 = Encoding.ASCII.GetBytes(isIncomingCall ? "2" : "1".ToString(1));         //1
            stream.Write(p4, 0, p4.Length);

            byte[] p5 = Encoding.ASCII.GetBytes(isHighPriority ? "1" : "0".ToString(1));         //1
            stream.Write(p5, 0, p5.Length);

            stream.Position = 0;

            req.Init(stream);

            return (CCResultCode)this.cci.SendMessage(req);          
        }

        public CCResultCode NewEmptyIncomingCallList()
        {
            if (this.cci == null)
            {
                return CCResultCode.CCERR_UNKNOWN_FAILURE;
            }

            NewIncomingCallReq req = new NewIncomingCallReq();

            MemoryStream stream = new MemoryStream();
            stream.Write(Encoding.ASCII.GetBytes("    "), 0, 4);
            stream.Write(Encoding.ASCII.GetBytes("0000"), 0, 4);

            stream.Position = 0;

            req.Init(stream);

            return (CCResultCode)this.cci.SendMessage(req);
        }

        public CCResultCode RemoveIncomingCall(string timeStamp, string callId, string aliasName, bool isIncomingCall, bool isHighPriority)
        {
            if (this.cci == null)
            {
                return CCResultCode.CCERR_UNKNOWN_FAILURE;
            }

            RmIncomingCall rmq = new RmIncomingCall();
            rmq.MsgSeq = "    ";
            rmq.NumOfSubMessages = "0001";

            MemoryStream stream = new MemoryStream();
            stream.Write(Encoding.ASCII.GetBytes("    "), 0, 4);
            stream.Write(Encoding.ASCII.GetBytes("0001"), 0, 4);

            byte[] p1 = Encoding.ASCII.GetBytes(timeStamp.ToString(14));       //14
            stream.Write(p1, 0, p1.Length);

            byte[] p2 = Encoding.ASCII.GetBytes(callId.ToString(16));           //16
            stream.Write(p2, 0, p2.Length);

            byte[] p3 = Encoding.Unicode.GetBytes(aliasName.ToString(20));      //40
            stream.Write(p3, 0, p3.Length);

            byte[] p4 = Encoding.ASCII.GetBytes(isIncomingCall ? "2" : "1".ToString(1));         //1
            stream.Write(p4, 0, p4.Length);

            byte[] p5 = Encoding.ASCII.GetBytes(isHighPriority ? "1" : "0".ToString(1));         //1
            stream.Write(p5, 0, p5.Length);

            stream.Position = 0;
            bool a = rmq.Init(stream);


            return (CCResultCode)this.cci.SendMessage(rmq);   
        }

        public CCResultCode NewOperationalMessage(string timeStamp, string operationalMessageId, string description, bool isHighPriority)
        {
            if (this.cci == null)
            {
                return CCResultCode.CCERR_UNKNOWN_FAILURE;
            }

            NewOperationalMessage msg = new NewOperationalMessage();
            msg.MsgSeq = "    ";
            msg.NumOfSubMessages = "0001";

            MemoryStream stream = new MemoryStream();
            stream.Write(Encoding.ASCII.GetBytes("    "), 0, 4);
            stream.Write(Encoding.ASCII.GetBytes("0001"), 0, 4);

            byte[] p1 = Encoding.ASCII.GetBytes(timeStamp.ToString(14));       //14
            stream.Write(p1, 0, p1.Length);

            byte[] p2 = Encoding.ASCII.GetBytes(operationalMessageId.ToString(12));           //12
            stream.Write(p2, 0, p2.Length);

            byte[] p3 = Encoding.ASCII.GetBytes(description.ToString(80));      //80
            stream.Write(p3, 0, p3.Length);

            byte[] p4 = Encoding.ASCII.GetBytes(isHighPriority ? "1" : "0".ToString(1));         //1
            stream.Write(p4, 0, p4.Length);

            stream.Position = 0;
            bool a = msg.Init(stream);

            return (CCResultCode)this.cci.SendMessage(msg);   
        }

        public CCResultCode NewEmptyOperaionalList()
        {
            if (this.cci == null)
            {
                return CCResultCode.CCERR_UNKNOWN_FAILURE;
            }

            NewOperationalMessage msg = new NewOperationalMessage();

            MemoryStream stream = new MemoryStream();
            stream.Write(Encoding.ASCII.GetBytes("    "), 0, 4);
            stream.Write(Encoding.ASCII.GetBytes("0000"), 0, 4);

            stream.Position = 0;

            bool a = msg.Init(stream);

            return (CCResultCode)this.cci.SendMessage(msg);
        }

       /* public int RemoveOperationalMessage(string timeStamp, string operationalMessageId, string subsystemUnit)
        {
            if (this.cci == null)
            {
                return (int)CCApiError.CCERR_UNKNOWN_FAILURE;
            }

            RmOperationalMessage msg = new RmOperationalMessage();
            msg.MsgSeq = "    ";
            msg.NumOfSubMessages = "0001";

            MemoryStream stream = new MemoryStream();
            stream.Write(Encoding.ASCII.GetBytes("    "), 0, 4);
            stream.Write(Encoding.ASCII.GetBytes("0001"), 0, 4);

            byte[] p1 = Encoding.ASCII.GetBytes(timeStamp.ToString(14));       //14
            stream.Write(p1, 0, p1.Length);

            byte[] p2 = Encoding.ASCII.GetBytes(operationalMessageId.ToString(12));           //12
            stream.Write(p2, 0, p2.Length);

            byte[] p3 = Encoding.ASCII.GetBytes(subsystemUnit.ToString(25));      //25
            stream.Write(p3, 0, p3.Length);

            stream.Position = 0;
            bool a = msg.Init(stream);

            return (int)this.cci.SendMessage(msg);   
        }*/
      
        private CCResultCode Convert(CCApiError error)
        {
            switch(error)
            {
                case CCApiError.SUCCESS:
                    return CCResultCode.SUCCESS;
                case CCApiError.CCERR_NO_CONNECTION:
                    return CCResultCode.CCERR_NO_CONNECTION;
                case CCApiError.CCERR_TIMEOUT:
                    return CCResultCode.CCERR_TIMEOUT;
                case CCApiError.CCERR_INVALID_SYSTEM:
                    return CCResultCode.CCERR_INVALID_SYSTEM;
                case CCApiError.CCERR_LOAD_LIB_FAILED:
                    return CCResultCode.CCERR_LOAD_LIB_FAILED;
                case CCApiError.CCERR_SETUP_FAIL:
                    return CCResultCode.CCERR_SETUP_FAIL;
                case CCApiError.CCERR_UNKNOWN_SUBSYSTEM:
                    return CCResultCode.CCERR_UNKNOWN_SUBSYSTEM;
                case CCApiError.CCERR_UNKNOWN_MESSAGE_FORMAT:
                    return CCResultCode.CCERR_UNKNOWN_MESSAGE_FORMAT;
                case CCApiError.CCERR_UNKNOWN_FAILURE:
                    return CCResultCode.CCERR_UNKNOWN_FAILURE;
                default :
                    return CCResultCode.CCERR_UNKNOWN_FAILURE;
            }
        }

        private void OnMessageReceived(IBaseMessage msg)
        {
            if(this.subsystem == null)
            {
                return;
            }

            CCMessageHandler handler;
            if(this.handlers.TryGetValue(msg.MsgType, out handler))
            {
                handler.Run(this.cci, this.subsystem, msg);
            }
        }

        private void InitializeHandlers()
        {
            this.handlers = new Dictionary<string, CCMessageHandler>();
            this.handlers[CCApiMessages.MTLogIn] = new LoginHandler();
            this.handlers[CCApiMessages.MTLogOut] = new LogoutHandler();
            this.handlers[CCApiMessages.MTSetView] = new CCSetViewHandler();
            this.handlers[CCApiMessages.MTHeartbeat] = new HeartbeatHandler();
            this.handlers[CCApiMessages.MTSetInput] = new CCSetInputHandler();
            this.handlers[CCApiMessages.MTSetDimension] = new CCSetDimensionHandler();
            this.handlers[CCApiMessages.MTSetPosition] = new CCSetPositionHandler();
            this.handlers[CCApiMessages.MTApplicationError] = new ApplicationErrorHandler();
            this.handlers[CCApiMessages.MTChangeUserID] = new CCChangeUserIdHandler();
            this.handlers[CCApiMessages.MTAccessPrivilege] = new CCAccessPrivilegeHandler();
            this.handlers[CCApiMessages.MTReqIncomingCallReqList] = new CCNewEmptyIncomingCallListHandler();
            this.handlers[CCApiMessages.MTReqOutstandingOpMsgList] = new CCNewEmptyOperationalListHandler();
        }
    }
}
