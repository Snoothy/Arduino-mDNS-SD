#include "UCR.h"
#include <ESP8266WiFi.h>
#include <ESP8266mDNS.h>
#include <WiFiUdp.h>
//#include <ArduinoJson.h>

#define MSG_HEARTBEAT 0
#define MSG_DESCRIPTOR 1
#define MSG_DATA 2


char packetBuffer[UDP_TX_PACKET_MAX_SIZE]; //buffer to hold incoming packet,
char ReplyBuffer[] = "acknowledged\r\n";       // a string to send back

UCR::UCR(const char* ssid, const char* password){
   UCR(ssid, password, 8080);
}

UCR::UCR(const char* ssid, const char* password, unsigned int port){
    _ssid = ssid;
    _password = password;
    _port = port;
}

unsigned long UCR::lastUpdateMillis(){
    return _lastUpdateMillis;
}

void UCR::begin(){
    Serial.println("UCR begin");

    setupWiFi();
    setupMDNS();
    Udp.begin(_port);
}

void UCR::setupWiFi(){
    Serial.println("Setup WiFi");

    if (strlen(_name) == 0){
        sprintf(_hostString, "UCR_%06X", ESP.getChipId());
    } else {
        strncpy(_hostString, _name, (sizeof(_hostString)-1));
    }

    Serial.print("Hostname: ");
    Serial.println(_hostString);

    WiFi.hostname(_hostString);
    WiFi.mode(WIFI_STA);
    WiFi.begin(_ssid, _password);

    while (WiFi.status() != WL_CONNECTED) {
        delay(250);
        Serial.print(".");
    }

    Serial.print("Connected to ");
    Serial.println(_ssid);
    Serial.print("IP address: ");
    Serial.println(WiFi.localIP());
}

void UCR::setupMDNS(){
    Serial.println("Setup MDNS");
    if (!MDNS.begin(_hostString)) {
        Serial.println("Error setting up MDNS responder!");
    }
    Serial.println("mDNS responder started");
    MDNS.addService("ucr", "udp", _localPort); 
}

void UCR::name(const char* name) {
    strncpy(_name, name, (sizeof(_name)-1));
}

void UCR::addButton(const char* name, int index){
    *_buttonList[index] = name;
}

void UCR::update(){
    MDNS.update();
    if (receiveUdp()){
        _lastUpdateMillis = millis();
    }

    // TODO update data structs
}

bool UCR::receiveUdp() {
    int packetSize = Udp.parsePacket();
    if (packetSize) {

        int len = Udp.read(incomingPacketBuffer, 255);
        if (len > 0) {
            incomingPacketBuffer[len] = 0;
        }
        Serial.printf("UDP packet contents: %s\n", incomingPacketBuffer);

        Udp.beginPacket(Udp.remoteIP(), Udp.remotePort());
        Udp.write("ACK");
        Udp.endPacket();

        Serial.println();
        Serial.print("Got good packet!");
    }
}
