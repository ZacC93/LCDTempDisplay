/*
  LCD
*/

#include <LiquidCrystal.h> //imports LCD library

LiquidCrystal lcd(12,13,4,5,6,7);
String cpuTemp;
const char startOfNumberDelimiter = '<';
const char middleOfNumbersDelimiter = ',';
const char endOfNumberDelimiter   = '>';
  
void setup() {
  lcd.begin(16,2); // Initializes the interface to the LCD screen, specifies width and height.
  Serial.begin(9600); // Initialize Serial library at 9600 bps
  lcd.print("CPU Temp: ");
  lcd.setCursor(0,1);
  lcd.print("GPU Temp: ");
}

void processNumberC (const long c)
  {
  lcd.setCursor(10,0);
  lcd.print(c);
  lcd.print("C");
  }
  
void processNumberG (const long g)
  {
  lcd.setCursor(10,1);
  lcd.print(g);
  lcd.print("C");

  }  // end of processNumber
  
void processInput ()
  {
  static long receivedNumber = 0;
  
  byte b = Serial.read ();
  
  switch (b)
    {
    case middleOfNumbersDelimiter:  
        processNumberC (receivedNumber); 
        receivedNumber = 0; 
        break;

    case endOfNumberDelimiter:  
        processNumberG (receivedNumber); 
        break;

    // fall through to start a new number
    case startOfNumberDelimiter: 
      receivedNumber = 0; 
      break;
      
    case '0' ... '9': 
      receivedNumber *= 10;
      receivedNumber += b - '0';
      break;

    } // end of switch  
  }  // end of processInput

void loop() {
  
  while (Serial.available ())
    processInput ();
}
