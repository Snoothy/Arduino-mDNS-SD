/*
  ESP8266 mDNS-SD responder and query sample

  This is an example of announcing and finding services.

  Instructions:
  - Update WiFi SSID and password as necessary.
  - Flash the sketch to two ESP8266 boards
  - The last one powered on should now find the other.
*/

#include <ESP8266WiFi.h>
#include <ESP8266mDNS.h>

#ifndef STASSID
#define STASSID "TODO"
#define STAPSK  "TODO"
#endif

const char* ssid     = STASSID;
const char* password = STAPSK;
char hostString[16] = {0};

void setup() {
  Serial.begin(115200);
  delay(100);
  Serial.println("\r\nsetup()");

  sprintf(hostString, "ESP_%06X", ESP.getChipId());
  Serial.print("Hostname: ");
  Serial.println(hostString);
  WiFi.hostname(hostString);

  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(250);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected to ");
  Serial.println(ssid);
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());

  if (!MDNS.begin(hostString)) {
    Serial.println("Error setting up MDNS responder!");
  }
  Serial.println("mDNS responder started");
  MDNS.addService("ucr", "udp", 8080); // Announce esp tcp service on port 8080

  Serial.println("loop() next");
}

void loop() {
  // put your main code here, to run repeatedly:
  MDNS.update();
}
