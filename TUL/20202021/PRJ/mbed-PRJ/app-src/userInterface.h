#include "LCD_DISCO_F429ZI.h"
#include "Thread.h"
#include "mbed.h"
#include "mbed_rtc_time.h"
#include "stm32f429i_discovery_lcd.h"
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <ctime>

#define PHONE_NUMBER_LENGTH 12
#define PHONE_LINE 14 // First line to write phone number
#define BATTERY_LINE 0
#define EMPTY_LINE (uint8_t *)"                      "
LCD_DISCO_F429ZI lcd;

struct MotorcycleState {
  float batteryCharge = 0.0;
};

class UserInterface {
public:
  void init();
  void update(MotorcycleState &state);
  void showCall(char number[]);
  void hideCall();
  void missCall();

protected:
  //+1 for "+"" in start of phone number
  char lastCalledNumber[PHONE_NUMBER_LENGTH + 1];
  float showNumberFor = 60; // Secunds
  time_t startShowNumber = 0;
  bool missedCall = false;
  MotorcycleState lastState;
  void displayNumber();
  void showBatteryState(float charge);
  void clearLine(uint32_t line);
};

void UserInterface::displayNumber() {
  lcd.SetTextColor(missedCall ? LCD_COLOR_RED : LCD_COLOR_GREEN);
  lcd.DisplayStringAt(0, LINE(PHONE_LINE), (uint8_t *)"CALLING", CENTER_MODE);
  lcd.DisplayStringAt(0, LINE(PHONE_LINE + 1), (uint8_t *)lastCalledNumber,
                      CENTER_MODE);
}

void UserInterface::init() {
  lcd.Clear(LCD_COLOR_BLACK);
  lcd.SetBackColor(LCD_COLOR_BLACK);
  lcd.SetTextColor(LCD_COLOR_WHITE);
  BSP_LCD_SetFont(&Font20);
  set_time(1256729737);
}

void UserInterface::clearLine(uint32_t line) {
  lcd.DisplayStringAt(0, line, EMPTY_LINE, LEFT_MODE);
}

void UserInterface::showBatteryState(float charge) {
  char buffer[18];
  if (abs(charge - lastState.batteryCharge) > 1) {
    clearLine(LINE(BATTERY_LINE));
    lastState.batteryCharge = charge;
    sprintf(buffer, "Battery :%.0f%%", charge);
    lcd.DisplayStringAt(0, BATTERY_LINE, (uint8_t *)buffer, LEFT_MODE);
  }
}

void UserInterface::update(MotorcycleState &state) {
  lcd.SetTextColor(LCD_COLOR_WHITE);
  showBatteryState(state.batteryCharge);
  if (startShowNumber + showNumberFor < time(NULL)) {
    hideCall();
  }
}

void UserInterface::hideCall() {
  missedCall = false;
  startShowNumber = 0;
  clearLine(LINE(PHONE_LINE));
  clearLine(LINE(PHONE_LINE + 1));
}

void UserInterface::missCall() {
  missedCall = true;
  displayNumber();
}

void UserInterface::showCall(char *number) {
  memcpy(&lastCalledNumber[1], number, PHONE_NUMBER_LENGTH);
  lastCalledNumber[0] = '+';
  missedCall = false;
  startShowNumber = time(NULL);
  displayNumber();
}