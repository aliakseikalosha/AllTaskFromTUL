#include "memory.h"
#include <malloc.h>
#include <stdio.h>
#include <cerrno>
#define REPLACE_BYTES 1

int alloc(int size, float** destination) {
	*destination = (float*)malloc(size * sizeof(float));
	printf("malloc of %d\n", size * sizeof(float));
	return *destination == NULL ? -1 : 0;
}

void shuffle(float* source, int size) {
#if REPLACE_BYTES
	char c;
	int n = size / 2 * sizeof(float) / sizeof(char);
	for (int i = 0; i < n; i++)
	{
		c = ((char*)source)[i];
		((char*)source)[i] = ((char*)source)[n + i];
		((char*)source)[n + i] = c;
	}
#else
	float f;
	int n = size / 2;
	for (int i = 0; i < n; i++)
	{
		f = source[i];
		source[i] = source[n + i];
		source[n + i] = f;
	}
#endif// REPLACE_BYTE
}

int test(float* source, float* shuffled, int size) {
	int n = size / 2;
#if REPLACE_BYTES
	n *= sizeof(float) / sizeof(char);
#endif // REPLACE_BYTE

	for (int i = 0; i < n; i++)
	{
#if REPLACE_BYTES
		if (((char*)source)[i] != ((char*)shuffled)[n + i] && size*sizeof(float)/sizeof(char) < n + i) {
#else
		if (source[i] != shuffled[n + i] && size < n + i) {
#endif// REPLACE_BYTE
			return -1;
		}
	}
	return 0;
}

int freeMemory(void* source) {
	free(source);
	if (errno == 0) {
		return 0;
	}
	else {
		perror("Error printed by perror");
		return -1;
	}
}