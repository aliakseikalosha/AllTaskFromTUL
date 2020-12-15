#include "ThisThread.h"
#include "mbed.h"
#include "ResetReason.h"
#include "BufferedSerial.h"
#include <cstdio>
#include <string>
#include <cstdint>
#include "stm32469i_discovery_lcd.h"
BufferedSerial pc(USBTX, USBRX);

void displayReasonOfRestart()
{
    switch (ResetReason::get()) {
        case RESET_REASON_POWER_ON:
            BSP_LCD_DisplayStringAt(20, LINE(5), (uint8_t *)"Power ON", CENTER_MODE);
            break;
        case RESET_REASON_PIN_RESET:
            BSP_LCD_DisplayStringAt(20, LINE(5), (uint8_t *)"Hardware Pin", CENTER_MODE);
            break;
        case RESET_REASON_SOFTWARE:
            BSP_LCD_DisplayStringAt(20, LINE(5), (uint8_t *)"Software Reset", CENTER_MODE);
            break;
        case RESET_REASON_WATCHDOG:
            BSP_LCD_DisplayStringAt(20, LINE(5), (uint8_t *)"Watchdog", CENTER_MODE);
            break;
        default:
            BSP_LCD_DisplayStringAt(20, LINE(5), (uint8_t *)"Unknown Reset", CENTER_MODE);
            break;
    }
}

void resetFromSP(){
    printf("\n\npress R to restart.\n\n");
    char c;
    while (true) {
        if(pc.readable()){
            c = pc.getc();
            pc.putc(c);
            if (c =='r') {
                NVIC_SystemReset();
            }
        }
    }
}
// main() runs in its own thread in the OS
int main()
{
    BSP_LCD_Init();
    BSP_LCD_LayerDefaultInit(LTDC_ACTIVE_LAYER_FOREGROUND, LCD_FB_START_ADDRESS);
    BSP_LCD_SelectLayer(LTDC_DEFAULT_ACTIVE_LAYER);
    BSP_LCD_Clear(LCD_COLOR_BLACK);
    BSP_LCD_SetBackColor(LCD_COLOR_BLACK);
    BSP_LCD_SetTextColor(LCD_COLOR_WHITE);

    displayReasonOfRestart();

    pc.baud(9600);

    Watchdog &watchdog = Watchdog::get_instance();
    watchdog.start(5000);

    Thread readSP;
    readSP.start(resetFromSP);

    readSP.join();
}
