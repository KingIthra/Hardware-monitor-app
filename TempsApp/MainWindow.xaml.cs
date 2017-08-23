using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Management;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Timers;
using System.Management;
using Timer = System.Threading.Timer;
using System.Speech.Synthesis;
using System.Threading;
using OpenHardwareMonitor;
using OpenHardwareMonitor.Collections;
using OpenHardwareMonitor.Hardware;

namespace TempsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {




        public float temperature;

        public MainWindow()
        {
           

            InitializeComponent();


            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(5000);
            dispatcherTimer.Start();

           








        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            PerformanceCounter memPref = new PerformanceCounter("Memory", "Available MBytes");
            PerformanceCounter cpuPerf = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            var computer = new Computer();
            computer.CPUEnabled = true;
            computer.Open();
            

            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                       
                        if (sensor.SensorType == SensorType.Temperature)
                        temperature = sensor.Value.GetValueOrDefault();
                       
                    }
                }
            }

            temp.Content = temperature;
            cpuUsage.Content = (int)cpuPerf.NextValue() + "%";
            Thread.Sleep(100);
            temp.Content = temperature;
            cpuUsage.Content = (int) cpuPerf.NextValue() + "%";
           ramUsage.Content = memPref.NextValue() + "Mb";





        }



    }
}
