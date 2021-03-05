 using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Linq;
using Windows.System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Management;

namespace PCClubApp
{
    public class SystemController
    {
        public static async System.Threading.Tasks.Task appRunAsync(GameUnit app)
        {
            
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            /*startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = GameEnumConversion.App_path(app);*/
            // startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.Arguments = "-f j -o  + ex1 +  -z 1.0 -s y  + ex2";
            //startInfo.Arguments = "-applaunch 730";
            //Process.Start(startInfo);
            Uri uri = new Uri(app.Path);
            var promptOptions = new LauncherOptions();
            promptOptions.DesiredRemainingView = Windows.UI.ViewManagement.ViewSizePreference.UseNone;

            _ = await Launcher.LaunchUriAsync(uri, promptOptions);
        }

        public static string GetMacAddress()
        {

            Trace.WriteLine("-------MAC------");

            string result = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    Trace.WriteLine(nic.GetPhysicalAddress().ToString());
                    Trace.WriteLine(nic.Id);
                    result = nic.Id;
                }
            }
            return result;
/*
            return (
                        from nic in NetworkInterface.GetAllNetworkInterfaces()
                        where nic.OperationalStatus == OperationalStatus.Up
                        select nic.GetIPProperties().ToString()
                    ).FirstOrDefault();*/
        }

        public static void ActionComputer(ESystemAction _action)
        {
            ShutdownManager.BeginShutdown( _action == ESystemAction.SHUTDOWN ? ShutdownKind.Shutdown : ShutdownKind.Restart, TimeSpan.FromSeconds(1));
        }

        

    }

   
}
