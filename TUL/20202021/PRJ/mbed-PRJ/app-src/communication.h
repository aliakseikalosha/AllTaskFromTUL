#include "mbed.h"
#include <cstdint>

#define BUFFER_READ_SIZE 16
#define BAUD_SPEED 9600

Serial *transfer = nullptr;
Serial *debugTransfer = nullptr;
volatile bool newData = false;
volatile int bufferIndex = 0;
//+1 to add \0 at the end
char readBuffer[BUFFER_READ_SIZE + 1];
char data[BUFFER_READ_SIZE + 1];

class SerialCom {

public:
  SerialCom(Serial *tr, Serial *dbg) {
    transfer = tr;
    debugTransfer = dbg;
  }
  void update();
  void send(const uint8_t *data, int length);
  void init();

  void attach(void (*function)(const uint8_t *, int));

protected:
  void (*onRecivedCommand)(const uint8_t *, int);
  static void readDataBluetooth(int indx = 0);
};

void SerialCom::readDataBluetooth(int indx) {
  newData = true;
  /*
  while (transfer->readable()) {
    readBuffer[bufferIndex++] = transfer->getc();
    if (bufferIndex == BUFFER_READ_SIZE) {
      bufferIndex = 0; // OVERFLOW
    }
    readBuffer[bufferIndex] = '\0';
  }
  newData = true;
  */
}

void SerialCom::init() {
  transfer->baud(BAUD_SPEED);
  // transfer->attach(this->readDataBluetooth);//todo fix this error
  debugTransfer->printf("Bluetooth Start\r\n");
}

void SerialCom::send(const uint8_t *data, int length) {
  transfer->write(data, length, nullptr);
  debugTransfer->printf("Sent\n");
}

void SerialCom::attach(void (*function)(const uint8_t *, int)) {
  onRecivedCommand = function;
}

void SerialCom::update() {
  static long count = 0;
  static int recived = 0;
  if (transfer->readable()) { // todo remove after use of attach
    transfer->read((uint8_t *)readBuffer, (BUFFER_READ_SIZE - 1) * sizeof(char),
                   &readDataBluetooth);
  }
  if (newData) {
    /*//src https://os.mbed.com/forum/mbed/topic/26163/?page=1#comment-50033
  __disable_irq(); // dissable interrupt to be able to copy data from buffer
  memcpy(data, readBuffer, bufferIndex);
  recived = bufferIndex;
  bufferIndex = 0;
  __enable_irq(); // enable interrupt
  send((uint8_t *)data, sizeof(uint8_t) * recived);
  */
    if (onRecivedCommand != nullptr) {
      onRecivedCommand((uint8_t *)readBuffer, BUFFER_READ_SIZE);
    }
    /*
        recived = BUFFER_READ_SIZE;
        memcpy(data, readBuffer, recived);
        send((uint8_t *)data, sizeof(uint8_t) * recived);
        if (onRecivedCommand != nullptr) {
          onRecivedCommand((uint8_t *)data, recived);
        }
    */
    newData = false;
  }
  /*//Test sent to show that is still alive
    if (count > 1000000) {
    //debugTransfer->putc('N');
    //debugTransfer->putc('\n');
    //transfer->putc('w');
    //transfer->putc('\n');
    //transfer->putc('\0');
    transfer->write((uint8_t*)"haha",sizeof(char)*4,nullptr);
    debugTransfer->write((uint8_t*)"haha",sizeof(char)*4,nullptr);
    count = 0;
  }
  count++;
  */
}
