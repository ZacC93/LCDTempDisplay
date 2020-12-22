# LCDTempDisplay
2x16 LCD display of CPU and GPU temperature.

LCD to Arduino can be set up like this, just make sure to use 12 and 13 instead of 1 and 2 for RS and E because 1 will cause issues with serial ports.
Best to throw a 220 ohm resistor on the anode or cathode too, probably doesn't matter but it's good to be sure.
https://howtomechatronics.com/tutorials/arduino/lcd-tutorial/

Serial port is COM3 by default, you can change it in the config file. It also includes an option for update frequency, value is in ms.

Future plans include more options (screen size, more stats, toggle stats on or off and choose where they are displayed on the screen), maybe a form to easily change the layout.

This is just a personal project to get better at programming, you can use the code for whatever you want if it's somehow useful.
