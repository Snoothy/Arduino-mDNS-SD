/*
  UCR ESP8266 lib
*/

#include <WiFiUdp.h>

class UCR {

    public:
        UCR(const char* ssid, const char* password);
        UCR(const char* ssid, const char* password, unsigned int port);
        void begin();
        void update();
        void name(const char* name);
        unsigned long lastUpdateMillis();
    private:
        void setupWiFi();
        void setupMDNS();
        bool receiveUdp();

        WiFiUDP Udp;
        char _hostString[32] = {0};
        unsigned int _localPort = 8080;
        char _name[32] = {0};
        const char* _ssid;
        const char* _password;
        unsigned int _port;
        unsigned long _lastUpdateMillis;

        char incomingPacketBuffer[255]; 
};