#include "LCD_DISCO_F429ZI.h"
#include "communication.h"
#include "mbed.h"
#include "stm32f429i_discovery_lcd.h"
#include "userInterface.h"
#include <algorithm>
#include <cctype>
#include <cstdint>
#include <cstring>

#define DEBUG_MODE
#define BUFFER_READ_SIZE 16

Serial pc(USBTX, USBRX);
Serial blue(PC_10, PC_11); // TX,RX

SerialCom dataTransfer(&blue, &pc);
UserInterface ui;

void parsePhoneCommand(const char *cmd);
void parseCommand(const uint8_t *data, int length) {
  /*
  Communication codes
  0123456789abcdf
  D|date        |    - return last distance statistic day
  D|22012021    |    - get distance data from 22/01/2021

  B|date        |    - return last battery statistic date and time
  B|220120211400|    - get battery data from 22/01/2021 14:00

  T|000991234567|    - incoming call from number
  T|end         |    - incoming call stopped by user
  T|miss        |    - incoming call missed
  */
  for (int i = 0; i < length; i++) {
    pc.printf("%c", (char)data[i]);
  }
  pc.printf("\nlength %d\n", length);
  if (length != BUFFER_READ_SIZE) {
    // return;
  }
  switch (data[0]) {
  case 'B':
    if (data[2] == 'd') {
      // sent last battery data date
      dataTransfer.send((const uint8_t *)"B|d22012021   |", 16);
    }
    if (data[2] > 47 && data[2] < 59) {
      // sedn data from date
      // parse data and then send data
      dataTransfer.send((const uint8_t *)"B|220120211354076|", 19);
      dataTransfer.send((const uint8_t *)"B|220120211359078|", 19);
      dataTransfer.send((const uint8_t *)"B|220120211404080|", 19);
      dataTransfer.send((const uint8_t *)"B|220120211500100|", 19);
    }
    break;
  case 'D':
    if (data[2] == 'd') {
      // sent last battery data date
      dataTransfer.send((const uint8_t *)"D|d22012021   |", 16);
    }
    if (data[2] > 47 && data[2] < 59) {
      // sedn data from date
      // parse data and then send data
      dataTransfer.send((const uint8_t *)"D|2201202111.5400|", 19);
      dataTransfer.send((const uint8_t *)"D|2201202124.1000|", 19);
      dataTransfer.send((const uint8_t *)"D|2201202112.1111|", 19);
      dataTransfer.send((const uint8_t *)"D|22012021100.730|", 19);
    }
    break;
  case 'T':
    parsePhoneCommand((char *)data);
    break;
  }
}

void parsePhoneCommand(const char *cmd) {
  if (cmd[2] == 'e') {
    ui.hideCall();
  } else if (cmd[2] == 'm') {
    ui.missCall();
  } else {
    ui.showCall((char *)&cmd[2]);
  }
}

int main() {
  pc.printf("Bluetooth Start\r\n");

  // echo back characters and toggle the LED
  dataTransfer.init();
  dataTransfer.attach(&parseCommand);
  ui.init();
  MotorcycleState motoState;
  motoState.batteryCharge = 100;
  int counter = 0;
  while (1) {
    dataTransfer.update();
    if (counter > 20000) {
      ui.update(motoState);
      counter = 0;
    }
    counter++;
    /*
    // battery test
    if (motoState.batteryCharge > 0) {
      motoState.batteryCharge -= 0.01f;
    }
    */
  }
}