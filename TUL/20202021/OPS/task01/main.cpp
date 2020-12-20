* mbed Microcontroller Library
 * Copyright (c) 2019 ARM Limited
 * SPDX-License-Identifier: Apache-2.0
 */

#include "mbed.h"


// Blinking rate in milliseconds
#define BLINKING_RATE     50ms
#define ON                1;
#define OFF               0;
typedef Kernel::Clock::duration_u32 duration;


void blink(DigitalOut led, duration on_time,duration off_time){
    led = ON;
    ThisThread::sleep_for(on_time);
    led = OFF;
    ThisThread::sleep_for(off_time);
}

int main()
{
    // Initialise the digital pin LED1 as an output
    DigitalOut led(LED1);
    int i = 0;
    duration on_time;
    while (true) {
        on_time = BLINKING_RATE * i;
        i = ++i % 10;
        blink(led, on_time, BLINKING_RATE);
    }
}