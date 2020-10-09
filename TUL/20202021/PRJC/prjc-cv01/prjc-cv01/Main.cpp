#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
const char* testBinPath = "..\\testbin";
const char* testTxtPath = "..\\testtxt";
const char* pathToCopy1 = "kopie-1";
const char* pathToCopy2 = "kopie-2";

void createFile(const char* path, void(*writeInt)(FILE*, int)) {
	FILE* f = fopen(path, "w");
	for (size_t i = 0; i < 10; i++)
	{
		writeInt(f, i);
	}
	fclose(f);
}

void writeIntBinary(FILE* f, int data) {
	fwrite(&data, sizeof(data), 1, f);
}

void writeIntText(FILE* f, int data) {
	fprintf(f, " %d ", data);
}

void createTestFiles() {
	createFile(testBinPath, writeIntBinary);
	createFile(testTxtPath, writeIntText);
}

void copy(const char* path) {
	FILE* f;
	f = fopen(path, "r");
	fseek(f, 0L, SEEK_END);
	long size = ftell(f);
	printf("Size of file %s is %d\n", path, size);
	fseek(f, 0L, SEEK_SET);
	char buffer = '\0';

	FILE* f1 = fopen(pathToCopy1, "w");
	FILE* f2 = fopen(pathToCopy2, "w");
	for (long i = 0; i < size; i++)
	{
		fread(&buffer, sizeof(buffer), 1, f);

		if (i < size / 2) {
			printf("1. Have read %10d byte : {%s}\n", i, &buffer);
			fwrite(&buffer, sizeof(buffer), 1, f1);
		}
		else {
			printf("2. Have read %10d byte : {%s}\n", i, &buffer);
			fwrite(&buffer, sizeof(buffer), 1, f2);
		}
		
	}
	fclose(f1);
	fclose(f2);
}

int main() {
	createTestFiles();
	copy(testBinPath);
	//copy(testTxtPath);
	return 0;
}

