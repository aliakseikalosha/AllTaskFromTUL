#include "mbed.h"
#include <inttypes.h>
#include "stm32746g_discovery_lcd.h"
 
#if !defined(MBED_THREAD_STATS_ENABLED)
#error "Thread statistics not enabled"
#endif
 
#define MAX_THREAD_STATS        0x8

#define BLINKING_RATE     50ms
#define ON                1;
#define OFF               0;
#define END               '\0';  
typedef Kernel::Clock::duration_u32 duration;


Thread *threadTextUp;
Thread *threadTextDown;
Thread *thredLed;
Thread *thredButton;
DigitalOut led(LED1);
DigitalIn btn(BUTTON1);
bool redText = true;

static EventFlags idle_ef;


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

    threadTextUp = new Thread(osPriorityNormal, OS_STACK_SIZE, nullptr, "TextUp_thread");
    threadTextDown = new Thread(osPriorityNormal, OS_STACK_SIZE, nullptr, "TextDown_thread");
    thredLed = new Thread(osPriorityNormal, OS_STACK_SIZE, nullptr, "Led_thread");
    thredButton = new Thread(osPriorityNormal, OS_STACK_SIZE, nullptr, "Button_thread");
    
    
    threadTextUp->start(writeTextUp);
    threadTextDown->start(writeTextDown);
    thredLed->start(blink);
    thredButton->start(changeColor);

    // Sleep helps other created threads to run
    mbed_stats_thread_t *stats;
    int count;
while(true){
    ThisThread::sleep_for(5s);
    printf("=======THREAD INFO=======\n");
    stats = new mbed_stats_thread_t[MAX_THREAD_STATS];
    count = mbed_stats_thread_get_each(stats, MAX_THREAD_STATS);
    for (int i = 0; i < count; i++) {
        printf("ID: 0x%" PRIx32 "\n", stats[i].id);
        printf("Name: %s \n", stats[i].name);
        printf("State: %" PRId32 "\n", stats[i].state);
        printf("Priority: %" PRId32 "\n", stats[i].priority);
        printf("Stack Size: %" PRId32 "\n", stats[i].stack_size);
        printf("Stack Space: %" PRId32 "\n", stats[i].stack_space);
        printf("\n");
    }
}
   
    threadTextUp->terminate();
    threadTextDown->terminate();   
    thredLed->terminate();  
    thredButton->terminate();  
    return 0;
}

void changeColor(){
    btn.mode(PullDown);
    while(true)
    {
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
    while(true)
    {
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
    while(true)
    {
        led = ON;
        ThisThread::sleep_for(BLINKING_RATE*10);
        led = OFF;
        ThisThread::sleep_for(BLINKING_RATE);
    }
}
