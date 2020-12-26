using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PCClubApp
{
    public class SystemController
    {
        public static void appRun(GameUnits app)
        {

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = GameEnumConversion.App_path(app);
            // startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.Arguments = "-f j -o  + ex1 +  -z 1.0 -s y  + ex2";
            startInfo.Arguments = "-applaunch 730";
            Process.Start(startInfo);
        }

    }
}
