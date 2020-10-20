#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include "memory.h"
#define MAX_SIZE 1000
#define MIN_SIZE 1

int getSize() {
	int i;
	if (scanf("%d", &i) != 0 && i < MIN_SIZE || i > MAX_SIZE) {
		printf("%d is unsuported size it must betwean %d and %d\n", i, MIN_SIZE, MAX_SIZE);
		return getSize();
	}
	return i;
}

int main() {
	int size = getSize();
	printf("Trying to create allocation for float* of size %d\n", size);
	float* numbers = NULL, * shuffled = NULL;
	if (alloc(size, &numbers) == 0 && alloc(size, &shuffled) == 0) {
		printf("Fill numbers\n");
		for (size_t i = 0; i < size; i++)
		{
			numbers[i] = ((float)i) / 2;
			shuffled[i] = ((float)i) / 2;
		}
		shuffle(shuffled, size);
		if (test(numbers, shuffled, size) == 0) {
			printf("Pass test\n");
		}
		else {
			printf("shuffle function dont work.\n");
		}

		if (freeMemory(numbers) == 0 && freeMemory(shuffled) == 0) {
			printf("Now memory are free.\n");
		}
	}
	else {
		printf("Having problems with memory allocation.\n");
	}
	return 0;
}