using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCDTempService
{
    class Hardware
    {
        protected string temp;
        protected int hardwareIndex;
        protected int tempSensorIndex;

        public string GetTemp()
        {
            return temp;
        }
    }
}
