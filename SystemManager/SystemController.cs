using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PCClubApp
{
    class SystemController
    {


        public void appRun(GameUnits app)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "dcm2jpg.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "-f j -o  + ex1 +  -z 1.0 -s y  + ex2";
            Process.Start(startInfo);
        }

    }
}
