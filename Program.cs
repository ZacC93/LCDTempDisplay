using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;
using System.IO.Ports;
using System.Windows.Forms;


namespace LCDTempService
{
    static class Program
    {
        static string port = System.Configuration.ConfigurationManager.AppSettings["activePort"];
        static int sleep = int.Parse(System.Configuration.ConfigurationManager.AppSettings["delay"]);
        static string cpuTemp;
        static string gpuTemp;
        static SerialPort lcdSerialPort = new SerialPort(port);

        public class UpdateVisitor : IVisitor
        {
            public void VisitComputer(IComputer computer)
            {
                computer.Traverse(this);
            }

            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
            }

            public void VisitSensor(ISensor sensor) { }

            public void VisitParameter(IParameter parameter) { }
        }

        static void GetCPUTemp()
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer computer = new Computer();
            computer.Open();
            computer.CPUEnabled = true;
            computer.Accept(updateVisitor);
            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                if (computer.Hardware[i].HardwareType == HardwareType.CPU)
                {
                    foreach (var sensor in computer.Hardware[i].Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature && sensor.Name.Contains("Package"))
                        {
                            cpuTemp = sensor.Value.ToString();
                        }
                    }
                }
            }
            computer.Close();
        }
        static void GetGPUTemp()
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer computer = new Computer();
            computer.Open();
            computer.GPUEnabled = true;
            computer.Accept(updateVisitor);
            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                if (computer.Hardware[i].HardwareType == HardwareType.GpuNvidia)
                {
                    foreach (var sensor in computer.Hardware[i].Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            gpuTemp = sensor.Value.ToString();
                        }
                    }
                }
                //AMD is untested, I don't own one
                else if (computer.Hardware[i].HardwareType == HardwareType.GpuAti)
                {
                    foreach (var sensor in computer.Hardware[i].Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            gpuTemp = sensor.Value.ToString();
                        }
                    }
                }

            }
            computer.Close();
        }
        static void Main(string[] args)
        {
            if (lcdSerialPort.IsOpen)
            {
                lcdSerialPort.Close();
            }

            lcdSerialPort.Open();

            while (true)
            {
                lcdSerialPort.Write("<");//start delimiter
                GetCPUTemp();
                lcdSerialPort.Write(cpuTemp);
                lcdSerialPort.Write(",");//middle delimiter
                GetGPUTemp();
                lcdSerialPort.Write(gpuTemp);
                lcdSerialPort.Write(">");//end delimiter
                Thread.Sleep(sleep);
            }
        }
    }
}