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

        public string GetGPUPackageTemp()
        {
            return temp;
        }

        public void SetGPUHardwareIndex() //hardware index of CPU
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
                    hardwareIndex = i;
                }
            }
            computer.Close();
        }

        public void SetGPUtempSensorIndex() //GPU package temp
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

        public void GetGPUTemp() //CPU package temp
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer computer = new Computer();
            computer.Open();
            computer.GPUEnabled = true;
            computer.Accept(updateVisitor);
            temp = computer.Hardware[hardwareIndex].Sensors[tempSensorIndex].Value.ToString(); //tempSensorIndex should be 5 but it's 0, will deal with it later. fed up with this shit.
            computer.Close();
        }


    }
}
/*old shit amd part might be useful

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
                //AMD Works, will get confused if machine has both AMD and Nvidia GPU so it's disabled for now
                //else if (computer.Hardware[i].HardwareType == HardwareType.GpuAti)
                {
                    foreach (var sensor in computer.Hardware[i].Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            gpuTemp = sensor.Value.ToString();
                        }
                    }
                }
*/