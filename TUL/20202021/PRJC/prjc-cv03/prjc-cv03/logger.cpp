#include "logger.h"
#include <stdio.h>

int log(const char* message) {
#ifdef DEBUG
	FILE* f = fopen(LOG_PATH, "a");
	fseek(f, 0L, SEEK_END);
	fprintf(f, "\n");
	fprintf(f, message);
	fclose(f);
#endif // DEBUG
	return 0;
}