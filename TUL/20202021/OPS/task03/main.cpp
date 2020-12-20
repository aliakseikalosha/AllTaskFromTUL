#include "mbed.h"
#include "stm32746g_discovery_lcd.h"

#define BLINKING_RATE     50ms
#define ON                1;
#define OFF               0;
#define END               '\0';  
typedef Kernel::Clock::duration_u32 duration;


Thread threadTextUp;
Thread threadTextDown;
Thread thredLed;
Thread thredButton;
DigitalOut led(LED1);
DigitalIn btn(BUTTON1);
volatile bool redText = true;


void displayText(uint16_t x,uint16_t y,const char* message, Text_AlignModeTypdef aligment, uint32_t bgColor,uint32_t fgColor);
void changeColor();
void blink();
void writeText(uint16_t x,uint16_t y,const char* message, duration time);
void writeTextUp();
void writeTextDown();

int main()
{
    BSP_LCD_Init();
    BSP_LCD_LayerDefaultInit(LTDC_ACTIVE_LAYER, LCD_FB_START_ADDRESS);
    BSP_LCD_SelectLayer(LTDC_ACTIVE_LAYER);
    BSP_LCD_Clear(LCD_COLOR_BLACK);
    BSP_LCD_SetFont(&LCD_DEFAULT_FONT);

    displayText(50, 100, "Aliaksei Kalosha", CENTER_MODE, LCD_COLOR_WHITE, LCD_COLOR_RED);
    threadTextUp.start(writeTextUp);
    threadTextDown.start(writeTextDown);
    thredLed.start(blink);
    thredButton.start(changeColor);

    thredLed.join();
}

void changeColor(){
    btn.mode(PullDown);
    while(true){
        if(btn.read()){
            redText = false;
        }else{
            redText = true;
        }
    }
}

void writeTextUp(){
    writeText(10, 10, "Top text will be here.", BLINKING_RATE);
}

void writeTextDown(){
    writeText(10, 50, "Bottom text will be here.", BLINKING_RATE*2);
}

void writeText(uint16_t x,uint16_t y,const char* message, duration printTime){
    int size = strlen(message);
    while(true){
        for (int i = 0; i <= size ; i++){
            displayText(x, y, &message[size-i], LEFT_MODE, LCD_COLOR_WHITE, redText ? LCD_COLOR_RED : LCD_COLOR_BLUE);
            ThisThread::sleep_for(printTime);
        }
        ThisThread::sleep_for(printTime*size);
        displayText(x, y, message, CENTER_MODE, LCD_COLOR_BLACK, LCD_COLOR_BLACK);
    }
}


void displayText(uint16_t x ,uint16_t y,const char* message, Text_AlignModeTypdef aligment, uint32_t bgColor,uint32_t fgColor){

        BSP_LCD_SetBackColor(bgColor);
        BSP_LCD_SetTextColor(fgColor);
        BSP_LCD_DisplayStringAt(x, y, (uint8_t *)message, aligment);
}

void blink(){
    while(true){
        led = ON;
        ThisThread::sleep_for(BLINKING_RATE*10);
        led = OFF;
        ThisThread::sleep_for(BLINKING_RATE);
    }
}

