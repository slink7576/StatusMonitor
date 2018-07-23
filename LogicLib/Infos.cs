using System;
using System.Windows.Forms;

namespace LogicLib
{
    public static class Infos
    {
        #region Getters


        static PerformanceCounter cpuCounter;

        static bool isConnected()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }
        static string getLanguage()
        {
            return InputLanguage.CurrentInputLanguage.Culture.IetfLanguageTag.Split('-')[1];
        }
        static string getTime()
        {
            return DateTime.Now.ToShortTimeString();
        }
        static int getPower()
        {
            return (int)(SystemInformation.PowerStatus.BatteryLifePercent * 100);
        }
        static int getCurrentCpuUsage()
        {
            return (int)cpuCounter.NextValue();


        }

        static int getAvailableRAM()
        {
            return (int)(100 - ((info.AvailablePhysicalMemory * 100) / info.TotalPhysicalMemory));

        }
        static bool isCharging()
        {
            try
            {
                if (SystemInformation.PowerStatus.BatteryChargeStatus.ToString() == "Charging" || SystemInformation.PowerStatus.BatteryChargeStatus.ToString().Split(',')[1] == " Charging")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception c)
            {
                return false;
            }

        }
        #endregion
    }
}
