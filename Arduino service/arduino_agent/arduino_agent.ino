#include <Arduino.h>
#include "UCR.h"

#ifndef STASSID
#define STASSID "TODO"
#define STAPSK "TODO"
#endif

#define LED_BUILTIN 2

const char *ssid = STASSID;
const char *password = STAPSK;
unsigned int localPort = 8080;

UCR ucr(ssid, password, localPort);

// Other vars
unsigned long timeOfLastGoodPacket = 0;
unsigned long deadmanTimeout = 500;
int deadmanTriggered = 0;

void setup()
{
  Serial.begin(115200);
  delay(100);

  Serial.println("\r\nsetup()");

  pinMode(LED_BUILTIN, OUTPUT);

  ucr.name("UCR-ExampleAgent");
  ucr.addButton("LED", 0);
  ucr.addAxis("Accelerator", 0);
  ucr.addAxis("Steering", 1);
  ucr.addEvent("Horn", 0);

  ucr.begin();
}

void loop()
{
  ucr.update();

  digitalWrite(LED_BUILTIN, ucr.readButton(0) ? LOW : HIGH);

  if (millis() - ucr.lastUpdateMillis() <= deadmanTimeout)
  {
    deadmanTriggered = 0;
    Serial.print(".");
    delay(10);
  }
  else
  {
    if (!deadmanTriggered)
    {
      Serial.print(" Dead :-(");
      deadmanTriggered = 1;
    }
  }
}
