using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCClubApp
{
    public enum ESystemAction
    {
        [Description("SHUTDOWN")]
        SHUTDOWN = 1,
        [Description("REBOOT")]
        REBOOT = 2
    }
}
