#include "mbed.h"
#include "stm32746g_discovery_lcd.h"

InterruptIn sw(BUTTON1);
EventQueue queue(32 * EVENTS_EVENT_SIZE);
Thread t;

void rise_handler(void)
{
    queue.call(printf, "rise_handler in context %p\n", ThisThread::get_id()); 
    queue.call(BSP_LCD_SetTextColor,LCD_COLOR_RED);
    queue.call(BSP_LCD_FillCircle,BSP_LCD_GetXSize()/2, BSP_LCD_GetYSize()/2, 60);
}

void fall_handler(void)
{
    queue.call(printf,"fall_handler in context %p\n", ThisThread::get_id()); 
    queue.call(BSP_LCD_SetTextColor,LCD_COLOR_DARKGRAY);
    queue.call(BSP_LCD_FillCircle,BSP_LCD_GetXSize()/2, BSP_LCD_GetYSize()/2, 60);
}

int main()
{
    t.start(callback(&queue, &EventQueue::dispatch_forever));
    printf("Starting in context %p\r\n", ThisThread::get_id());

    sw.rise(rise_handler);
    sw.fall(fall_handler);

    BSP_LCD_Init();
    BSP_LCD_LayerDefaultInit(LTDC_ACTIVE_LAYER, LCD_FB_START_ADDRESS);
    BSP_LCD_SelectLayer(LTDC_ACTIVE_LAYER);
    BSP_LCD_Clear(LCD_COLOR_DARKGRAY);
}
