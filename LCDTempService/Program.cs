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
using System.Diagnostics;



namespace LCDTempService
{
    static class Program
    {
        static string port = System.Configuration.ConfigurationManager.AppSettings["activePort"];
        static int sleep = int.Parse(System.Configuration.ConfigurationManager.AppSettings["delay"]);
        static SerialPort lcdSerialPort = new SerialPort(port);    
        


        static void Main(string[] args)
        {
            CPU cpu = new CPU();
            GPU gpu = new GPU();

            if (lcdSerialPort.IsOpen)
            {
                lcdSerialPort.Close();
            }

            lcdSerialPort.Open();
            cpu.SetCPUHardwareIndex();
            cpu.SetCPUtempSensorIndex();
            gpu.SetGPUHardwareIndex();
            gpu.SetGPUtempSensorIndex();

            while (true)
            {
                lcdSerialPort.Write("<");//start delimiter
                cpu.SetCPUTemp();
                lcdSerialPort.Write(cpu.GetTemp());
                lcdSerialPort.Write(",");//middle delimiter
                gpu.SetGPUTemp();
                lcdSerialPort.Write(gpu.GetTemp());
                lcdSerialPort.Write(">");//end delimiter
                Thread.Sleep(sleep);
            }
        }
    }
}
