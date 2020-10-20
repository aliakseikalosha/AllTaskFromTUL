#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include "minus.h"
#include "plus.h"
#include "logger.h"
#include "counter.h"

int getNumber(const char* message, int* a);
int stop(char *yes, char *no);

int main() {
	int a, b, c, op, count_plus = 0, count_minus = 0;
	char logBuffer[100];
	char no = 'n', yes = 'y';
	while (true) {
		getNumber("\nEnter number a : ", &a);
		sprintf(logBuffer, "entered number \"a\" is %d", a);
		log(logBuffer);
		getNumber("\nEnter number b : ", &b);
		sprintf(logBuffer, "entered number \"b\" is %d", a);
		log(logBuffer);
		getNumber("\nSelect operation:\n1. +\n2. -\n", &op);
		sprintf(logBuffer, "selected operation is %d", op);
		log(logBuffer);
		switch (op)
		{
		case 1:
			c = plus(a, b);
			printf("a + b = %d", c);
			countOperation(NULL, &count_plus);
			break;
		case 2:
			c = minus(a, b);
			printf("a - b = %d", c);
			countOperation(&count_minus, NULL);
			break;
		default:
			sprintf(logBuffer, "Unknow operation: %d", op);
			log(logBuffer);
			break;
		}
		if (stop(&no, &yes)) {
			break;
		}
	}

	printf("Statistics:\n+ used %d times\n- used %d times", count_plus, count_minus);
}

int stop(char *yes, char *no) {
	char input;
	while (true) {
		while (getchar() != '\n');
		printf("\nEnd? (y/n)");
		scanf("%c", &input);
		if (input == *yes) {
			return 0;
		}
		else if (input == *no) {
			return 1;
		}
	}
}

int getNumber(const char* message, int* a) {
	printf(message);
	return scanf("%d", a);
}