using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis.cc4net
{
    public enum CCResultCode
    {
        SUCCESS,
        CCERR_GENERAL,
        CCERR_NO_CONNECTION,
        CCERR_TIMEOUT,
        CCERR_INVALID_SYSTEM,
        CCERR_LOAD_LIB_FAILED,
        CCERR_SETUP_FAIL,
        CCERR_UNKNOWN_SUBSYSTEM,
        CCERR_UNKNOWN_MESSAGE_FORMAT,
        CCERR_UNKNOWN_FAILURE
    }
}
