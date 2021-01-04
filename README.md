# LCDTempDisplay
2x16 LCD display of CPU and GPU temperature.

Requires OpenHardwareMonitorLib.dll
Easiest way to get it is to just install Open Hardware Monitor https://openhardwaremonitor.org/downloads/

LCD to Arduino can be set up like this: https://howtomechatronics.com/tutorials/arduino/lcd-tutorial/
Be sure to use 12 and 13 instead of 0 and 1 since on most boards 0 and 1 are serial pins.
Best to throw a 220 ohm resistor on the anode or cathode of the backlight too, probably doesn't matter but it's good to be sure.

Serial port is COM3 by default, you can change it in the config file. It also includes an option for update frequency, value is in ms. 

This is just a personal project to get better at programming, you can use the code for whatever you want if it's somehow useful.
