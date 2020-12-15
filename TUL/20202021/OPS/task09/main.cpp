#include "mbed.h"
#include "stm32746g_discovery_lcd.h"
#include "stm32746g_discovery_ts.h"
#include <cstdio>

Semaphore one_slot(1);
Thread t1;
Thread t2;
Thread t3;

void test_thread(void const *name)
{
    while (true) {
        one_slot.acquire();
        BSP_LCD_Clear(LCD_COLOR_BLUE);
        BSP_LCD_DisplayStringAt(0, LINE(5), (uint8_t *)name, CENTER_MODE);
    }
}
 
int main()
{

    TS_StateTypeDef TS_State;
    uint16_t x, y;
    uint8_t text[30];
    uint8_t status;
    uint8_t idx;
    uint8_t cleared = 0;
    uint8_t prev_nb_touches = 0;
 
    BSP_LCD_Init();
    BSP_LCD_LayerDefaultInit(LTDC_ACTIVE_LAYER, LCD_FB_START_ADDRESS);
    BSP_LCD_SelectLayer(LTDC_ACTIVE_LAYER);
 
    BSP_LCD_DisplayStringAt(0, LINE(5), (uint8_t *)"TOUCHSCREEN DEMO", CENTER_MODE);
    HAL_Delay(1000);
 
    status = BSP_TS_Init(BSP_LCD_GetXSize(), BSP_LCD_GetYSize());
    if (status != TS_OK) {
        BSP_LCD_Clear(LCD_COLOR_RED);
        BSP_LCD_SetBackColor(LCD_COLOR_RED);
        BSP_LCD_SetTextColor(LCD_COLOR_WHITE);
        BSP_LCD_DisplayStringAt(0, LINE(5), (uint8_t *)"TOUCHSCREEN INIT FAIL", CENTER_MODE);
    } else {
        BSP_LCD_Clear(LCD_COLOR_GREEN);
        BSP_LCD_SetBackColor(LCD_COLOR_GREEN);
        BSP_LCD_SetTextColor(LCD_COLOR_WHITE);
        BSP_LCD_DisplayStringAt(0, LINE(5), (uint8_t *)"TOUCHSCREEN INIT OK", CENTER_MODE);
    }
 
    HAL_Delay(1000);
    BSP_LCD_SetFont(&Font12);
    BSP_LCD_SetBackColor(LCD_COLOR_BLUE);
    BSP_LCD_SetTextColor(LCD_COLOR_WHITE);
    
    t1.start(callback(test_thread, (void *)"Vlakno 1 ma token"));
    t2.start(callback(test_thread, (void *)"Vlakno 2 ma token"));
    t3.start(callback(test_thread, (void *)"Vlakno 3 ma token"));
    bool wasTouched = false; 
    while(1) {
        BSP_TS_GetState(&TS_State);
        if (TS_State.touchDetected)
        {
            if(!wasTouched) {
                wasTouched = true;
                one_slot.release();
            }
        }else{
            wasTouched = false;
        }
    }
}