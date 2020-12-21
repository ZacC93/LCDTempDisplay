# LCDTempDisplay
2x16 LCD display of CPU and GPU temperature.

LCD to Arduino can be set up like this, just make sure to use 12 and 13 instead of 1 and 2 for RS and E because 1 will cause issues with serial ports.
Best to throw a 220 ohm resistor on the anode or cathode too, probably doesn't matter but it's good to be sure.
https://howtomechatronics.com/tutorials/arduino/lcd-tutorial/

Potentiometer can be replaced with code if you don't have one, I didn't bother but might add it later. Should be pretty easy to find a guide.

Serial port is COM3 by default, you can change it in the config file. It also includes an option for update frequency, value is in ms.
