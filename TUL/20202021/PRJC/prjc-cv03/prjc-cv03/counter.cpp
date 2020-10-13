#include "counter.h"
#include <cstddef>

void countOperation(int* minus, int* plus) {
	if (minus != NULL) (*minus)++;
	if (plus != NULL) (*plus)++;
}