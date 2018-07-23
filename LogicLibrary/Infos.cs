using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogicLibrary
{
    public static class Infos
    {


        static ComputerInfo info = new ComputerInfo();
        static PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        #region Getters

        public static string getName()
        {
            return SystemInformation.ComputerName.ToString();
        }


        public static bool hasBattery()
        {
            PowerStatus pwr = SystemInformation.PowerStatus;
  
            if (SystemInformation.PowerStatus.BatteryChargeStatus.ToString() == "NoSystemBattery")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool isConnected()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }
        public static string getLanguage()
        {
            return InputLanguage.CurrentInputLanguage.Culture.IetfLanguageTag.Split('-')[1];
        }
        public static string getTime()
        {
            return DateTime.Now.ToShortTimeString();
        }
        public static int getPower()
        {
            return (int)(SystemInformation.PowerStatus.BatteryLifePercent * 100);
        }
        public static int getCurrentCpuUsage()
        {
            return (int)cpuCounter.NextValue();


        }
        public static string OSName()
        {
            return info.OSFullName;
        }

        public static int getAvailableRAM()
        {
            return (int)(100 - ((info.AvailablePhysicalMemory * 100) / info.TotalPhysicalMemory));

        }
        public static bool isCharging()
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
