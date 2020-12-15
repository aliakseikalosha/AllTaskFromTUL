#include "LCD_DISCO_F469NI.h"
#include "LowPowerTicker.h"
#include "TS_DISCO_F469NI.h"
#include "ThisThread.h"
#include "mbed.h"
#include "mbed_power_mgmt.h"
#include "stm32469i_discovery_lcd.h"
#include <algorithm>
#include <cstdint>
#include <cstdio>

LCD_DISCO_F469NI lcd;
TS_DISCO_F469NI ts;

#define DEFAULT_DELAY 10

class Button {
public:
  Button(uint16_t x_, uint16_t y_, uint16_t sx_, uint16_t sy_,const char *message_)
      : x(x_), y(y_), sx(sx_), sy(sy_), message(message_) {
    wasPress = false;
  }

  bool checkForPress(uint16_t px, uint16_t py) {
    wasPress = false;
    if (px > x && px < x + sx && py > y && py < y + sy) {
      wasPress = true;
    }
    return wasPress;
  }

  void Draw() {
    uint32_t color = lcd.GetTextColor();
    uint32_t color_b = lcd.GetBackColor();
    lcd.SetTextColor(wasPress ? LCD_COLOR_DARKCYAN : LCD_COLOR_CYAN);
    lcd.FillRect(x, y, sx, sy);
    sFONT *f = lcd.GetFont();
    lcd.SetFont(&Font24);
    lcd.SetTextColor(wasPress ? LCD_COLOR_DARKYELLOW : LCD_COLOR_DARKYELLOW);
    lcd.SetBackColor(wasPress ? LCD_COLOR_DARKCYAN : LCD_COLOR_CYAN);
    lcd.DisplayStringAt(x + sx / 2-6*sizeof(message)/sizeof(char), y + sy / 2 - 12, (uint8_t *)message, LEFT_MODE);
    lcd.SetFont(f);
    lcd.SetTextColor(color);
    lcd.SetBackColor(color_b);
  }

private:
  uint16_t x, y, sx, sy;
  const char *message;
  bool wasPress;
};

bool oldBState[3] = {true, true, true};
bool currentBState[3] = {false, false, false};
Button *setB = new Button(400, 100, 205, 100, "SET");
Button *plusB = new Button(400, 205, 100, 100, "+");
Button *minusB = new Button(505, 205, 100, 100, "-");
uint8_t text[30];

InterruptIn resetTimer(BUTTON1);
LowPowerTicker redScreenTimer;

volatile uint64_t delay = 0;
volatile bool reset = false;

void updateButtons(int16_t x, int16_t y);
void drawButton();
void displayTime(uint64_t time,bool showDel);
void showRedScreen();

void drawButton() {
  plusB->Draw();
  minusB->Draw();
  setB->Draw();
}

void displayTime(uint64_t time,bool showDel){
    lcd.DisplayStringAt(20, LINE(5), (uint8_t *)"       ", LEFT_MODE);
    sprintf((char *)text, showDel?"%llu:%02llu":"%llu %02llu", time/60,time%60);
    lcd.DisplayStringAt(20, LINE(5), (uint8_t *)&text, LEFT_MODE);
}

void resetTime() {
    redScreenTimer.detach();
    delay = DEFAULT_DELAY; 
    reset = true;
    lcd.Clear(LCD_COLOR_BLUE);
    updateButtons(-1, -1);
    drawButton();
    displayTime(delay, true);
}

void startTimer(){
    updateButtons(-1, -1);
    redScreenTimer.attach(&showRedScreen,delay);
    reset = false;
    uint64_t i = delay;
    while (!reset && i>=1) {
        if(!reset){
            displayTime(i, false);
            ThisThread::sleep_for(500);
        }
        if(!reset){
            displayTime(i, true);
            ThisThread::sleep_for(500);
        }
        i--;
    }
    while (!reset) { }
}
void showRedScreen(){
    lcd.Clear(LCD_COLOR_RED);
}

void updateButtons(int16_t x, int16_t y) {
  currentBState[0] = plusB->checkForPress(x, y);
  currentBState[1] = minusB->checkForPress(x, y);
  currentBState[2] = setB->checkForPress(x, y);
  if (oldBState[0] != currentBState[0]) {
    plusB->Draw();
    if(currentBState[0]){
        delay++;
        displayTime(delay, true);
    }
  }
  if (oldBState[1] != currentBState[1]) {
    minusB->Draw();
    if(delay>1 && currentBState[1]){
        delay--;
        displayTime(delay, true);
    }
  }
  if (oldBState[2] != currentBState[2]) {
    setB->Draw();
    if (currentBState[2]) {
      startTimer();
    }
  }
  for (uint8_t i = 0; i < 3; i++) {
    oldBState[i] = currentBState[i];
  }
}

int main() {
  resetTimer.fall(resetTime);
  TS_StateTypeDef TS_State;
  uint16_t x, y;
  uint8_t status;
  uint8_t idx;
  uint8_t cleared = 0;
  uint8_t prev_nb_touches = 0;
  BSP_LCD_SetFont(&Font24);

  status = ts.Init(lcd.GetXSize(), lcd.GetYSize());
  if (status != TS_OK) {
    lcd.Clear(LCD_COLOR_RED);
    lcd.SetBackColor(LCD_COLOR_RED);
    lcd.SetTextColor(LCD_COLOR_WHITE);
    lcd.DisplayStringAt(0, LINE(5), (uint8_t *)"TOUCHSCREEN INIT FAIL", CENTER_MODE);
    while(1){ }
  }
  lcd.SetBackColor(LCD_COLOR_BLUE);
  lcd.SetTextColor(LCD_COLOR_WHITE);
  lcd.Clear(LCD_COLOR_BLUE);
  drawButton();
  displayTime(delay, true);
  ThisThread::sleep_for(1000);
  while (1) {
    ts.GetState(&TS_State);
    if (TS_State.touchDetected) {
      if (TS_State.touchDetected > 1) {
        //lcd.DisplayStringAt(0, LINE(0), (uint8_t *)"Error multitouch not suported", LEFT_MODE);
        updateButtons(-1, -1);
      } else if (TS_State.touchDetected > 0) {
        x = TS_State.touchX[0];
        y = TS_State.touchY[0];
        updateButtons(x, y);
      }
    } else {
      updateButtons(-1, -1);
    }
  }
}
