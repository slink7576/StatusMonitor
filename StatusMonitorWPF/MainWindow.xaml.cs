﻿using System;
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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Media;


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
        static List<Rectangle> NetItems;
        static List<Rectangle> PwrItems;
        static Settings curr;
        public  MainWindow()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Settings));

            if (!File.Exists("interface.json"))
            {
                 curr = new Settings() { ColorR = 253, ColorB = 199, ColorG = 129, Font = "Tahoma" };
              

                using (FileStream fs = new FileStream("interface.json", FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, curr);
                }
            }


           
           try
           {
                using (FileStream fs = new FileStream("interface.json", FileMode.Open))
                {
                    curr = (Settings)jsonFormatter.ReadObject(fs);
                
                }
            }
            catch (Exception c)
                {
                    System.Windows.Forms.MessageBox.Show("Corrupted interface file. Please restart an application.");
                    File.Delete("interface.json");
                    Environment.Exit(0);
            }
               
          


        








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



            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, (byte)curr.ColorR, (byte)curr.ColorG, (byte)curr.ColorB));





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

            NetItems = new List<Rectangle>();


            NetItems.Add(N0);
            NetItems.Add(N1);
            NetItems.Add(N2);
            NetItems.Add(N3);
            NetItems.Add(N4);
            NetItems.Add(N5);
            NetItems.Add(N6);




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
                PwrLabel.Foreground = brush;
                PwrBar.Stroke = brush;
                foreach (Rectangle c in PwrItems)
                {
                    c.Stroke = brush;
                    c.Fill = brush;
                }
            }

            var font = new FontFamily(curr.Font);

            TIME.FontFamily = font;
            OS.FontFamily = font;
            NAME.FontFamily = font;
            LNG.FontFamily = font;

            CPU.FontFamily = font;
            RAM.FontFamily = font;
            BTR.FontFamily = font;

            CpuLabel.FontFamily = font;
            PwrLabel.FontFamily = font;
            RamLabel.FontFamily = font;



            TIME.Foreground = brush;
          
            OS.Foreground = brush;
            NAME.Foreground = brush;
            CPU.Foreground = brush;
            RAM.Foreground = brush;
            BTR.Foreground = brush;
            LNG.Foreground = brush;





            CpuLabel.Foreground = brush;
            RamLabel.Foreground = brush;
           

            CpuBar.Stroke = brush;
            RamBar.Stroke = brush;
           
            NetBar.Stroke = brush;
            LngBar.Stroke = brush;




            foreach (Rectangle c in NetItems)
            {
                c.Stroke = brush;
                c.Fill = brush;
            }
            foreach (Rectangle c in CpuItems)
            {
                c.Stroke = brush;
                c.Fill = brush;
            }
            foreach (Rectangle c in RamItems)
            {
                c.Stroke = brush;
                c.Fill = brush;
            }
           




























            tasks.Add(CustomAnimation.SetBarStaticGeneric(RamItems, Infos.getAvailableRAM, RAM, () => { return false; }));
            tasks.Add(CustomAnimation.SetBarStaticGeneric(CpuItems, Infos.getCurrentCpuUsage, CPU, () => { return false; }));
            tasks.Add(CustomAnimation.Switch(NETW, Infos.isConnected));
            tasks.Add(CustomAnimation.SetData(LNG, Infos.getLanguage));
            tasks.Add(CustomAnimation.SetData(TIME, Infos.getTime));


            RunTasks(tasks);




        }


        static void InitializeTheme(Settings sett, Window wind)
        {

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
