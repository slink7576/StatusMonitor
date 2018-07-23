using System;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using System.Windows.Controls;

using System.Collections.Generic;
using System.Windows.Shapes;
using System.Globalization;
using LogicLibrary;

namespace StatusMonitorWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {
     
       
       
        
        static List<Rectangle> CpuItems;
        static List<Rectangle> RamItems;
       
        static List<Rectangle> PwrItems;
       
        public  MainWindow()
        {

            var tasks = new List<Task>();


            
           
         
            
           
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
           
           
          
            InitializeComponent();
         
            NAME.Content = Infos.getName();
            OS.Text = Infos.OSName();

            if (!Infos.hasBattery())
                this.Width = 305;

           //  this.Top = 100;
           // this.Left = 100;


            this.Top = screenHeight - this.Height;
            this.Left = screenWidth - this.Width;
            this.ShowInTaskbar = false;

            CpuItems = new List<Rectangle>();

            CpuItems.Add(C15);
            CpuItems.Add(C14);
            CpuItems.Add(C13);
            CpuItems.Add(C12);
            CpuItems.Add(C11);
            CpuItems.Add(C10);
            CpuItems.Add(C9);
            CpuItems.Add(C8);
            CpuItems.Add(C7);
            CpuItems.Add(C6);
            CpuItems.Add(C5);
            CpuItems.Add(C4);
            CpuItems.Add(C3);
            CpuItems.Add(C2);
            CpuItems.Add(C1);

            RamItems = new List<Rectangle>();

            RamItems.Add(R15);
            RamItems.Add(R14);
            RamItems.Add(R13);
            RamItems.Add(R12);
            RamItems.Add(R11);
            RamItems.Add(R10);
            RamItems.Add(R9);
            RamItems.Add(R8);
            RamItems.Add(R7);
            RamItems.Add(R6);
            RamItems.Add(R5);
            RamItems.Add(R4);
            RamItems.Add(R3);
            RamItems.Add(R2);
            RamItems.Add(R1);


            if (Infos.hasBattery())
            {

                PwrItems = new List<Rectangle>(); 
                PwrItems.Add(B15);
                PwrItems.Add(B14);
                PwrItems.Add(B13);
                PwrItems.Add(B12);
                PwrItems.Add(B11);
                PwrItems.Add(B10);
                PwrItems.Add(B9);
                PwrItems.Add(B8);
                PwrItems.Add(B7);
                PwrItems.Add(B6);
                PwrItems.Add(B5);
                PwrItems.Add(B4);
                PwrItems.Add(B3);
                PwrItems.Add(B2);
                PwrItems.Add(B1);
                tasks.Add(CustomAnimation.SetBarStaticGeneric(PwrItems, Infos.getPower, BTR, Infos.isCharging));
            }


            
            tasks.Add(CustomAnimation.SetBarStaticGeneric(RamItems, Infos.getAvailableRAM, RAM, () => { return false; }));
            tasks.Add(CustomAnimation.SetBarStaticGeneric(CpuItems, Infos.getCurrentCpuUsage, CPU, () => { return false; }));
            tasks.Add(CustomAnimation.Switch(NETW, Infos.isConnected));
            tasks.Add(CustomAnimation.SetData(LNG, Infos.getLanguage));
            tasks.Add(CustomAnimation.SetData(TIME, Infos.getTime));


            RunTasks(tasks);




        }



        static async void RunTasks(List<Task> tasks)
        {

            foreach (Task gh in tasks)
            {
                await gh;
            }

        }




    }
}
