using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCDTempService
{
    class CPU : Hardware
    {

        public int GetCPUHardwareIndex()
        {
            return hardwareIndex;
        }

        public int GetCPUTempSensorIndex()
        {
            return tempSensorIndex;
        }

        public string GetCPUPackageTemp()
        {
            return temp;
        }

        public void SetCPUHardwareIndex() //Set hardware index of CPU
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
                    hardwareIndex = i;
                }
            }
            computer.Close();
        }

        public void SetCPUtempSensorIndex() //Set temperature sensor index of CPU
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer computer = new Computer();
            computer.Open();
            computer.CPUEnabled = true;
            computer.Accept(updateVisitor);
            for (int i = 0; i < computer.Hardware[hardwareIndex].Sensors.Length; i++)
            {
                if (computer.Hardware[hardwareIndex].Sensors[i].SensorType == SensorType.Temperature 
                    && computer.Hardware[hardwareIndex].Sensors[i].Name.Equals("CPU Package"))
                {
                    tempSensorIndex = i;
                }
            }
            computer.Close();
        }

        public void SetCPUTemp() //Set CPU package temp
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer computer = new Computer();
            computer.Open();
            computer.CPUEnabled = true;
            computer.Accept(updateVisitor);
            temp = computer.Hardware[hardwareIndex].Sensors[tempSensorIndex].Value.ToString(); 
            computer.Close();
        }
    }
}
