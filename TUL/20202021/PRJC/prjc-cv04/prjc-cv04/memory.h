#pragma once

int alloc(int size, float** destination);
void shuffle(float* source, int size);
int test(float* source, float* shuffled, int size);
int freeMemory(void* source);