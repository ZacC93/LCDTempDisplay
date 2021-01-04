using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCDTempService
{
    class GPU : Hardware
    {       

        public int GetGPUHardwareIndex()
        {
            return hardwareIndex;
        }

        public int GetGPUTempSensorIndex()
        {
            return tempSensorIndex;
        }

        public string GetGPUPackageTemp()
        {
            return temp;
        }

        public void SetGPUHardwareIndex() //Set hardware index of GPU
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer computer = new Computer();
            computer.Open();
            computer.GPUEnabled = true;
            computer.Accept(updateVisitor);
            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                if (computer.Hardware[i].HardwareType == HardwareType.GpuNvidia) //GpuAti for AMD cards
                {
                    hardwareIndex = i;
                }
            }
            computer.Close();
        }

        public void SetGPUtempSensorIndex() //Set temperature sensor index of GPU
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer computer = new Computer();
            computer.Open();
            computer.GPUEnabled = true;
            computer.Accept(updateVisitor);
            for (int i = 0; i < computer.Hardware[hardwareIndex].Sensors.Length; i++)
            {
                if (computer.Hardware[hardwareIndex].Sensors[i].SensorType == SensorType.Temperature)
                {
                    tempSensorIndex = i;
                }
            }
            computer.Close();
        }

        public void SetGPUTemp() //Set GPU package temp
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer computer = new Computer();
            computer.Open();
            computer.GPUEnabled = true;
            computer.Accept(updateVisitor);
            temp = computer.Hardware[hardwareIndex].Sensors[tempSensorIndex].Value.ToString(); 
            computer.Close();
        }
    }
}
