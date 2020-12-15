#include "mbed.h"
#include <cstdint>

static BufferedSerial pc(USBTX, USBRX);
DigitalIn btn(BUTTON1);

void printToSerialPort(){
    btn.mode(PullDown);
    char messageGreating[] = "\nPip-Pip\nYou have one new message.\nPress the button to send it via serial port...\n";
    char messageType[] = "Hello user!\nYour doing great, have a nice day!\nBye:]\nPress the button to read again\n";
    uint8_t i = 0, size = sizeof(messageType), lastState = btn.read(),
    currentState;
    pc.write(messageGreating,sizeof(messageGreating));
    while(true){
        currentState = btn.read();
        if (currentState != lastState) {
            lastState = currentState;
            if(currentState){
              pc.write(&messageType[i], sizeof(char));
              i = ++i % size;
            }
        }
    }
}

    Thread buttonThred;

int main()
{
    buttonThred.start(printToSerialPort);
    buttonThred.join();
}
