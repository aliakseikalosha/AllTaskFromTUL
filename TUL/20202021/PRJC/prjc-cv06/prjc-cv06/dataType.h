#pragma once
#define _CRT_SECURE_NO_WARNINGS
#include "basicVec.h"
#include <malloc.h>

#include <stdio.h>
#include <string.h>
#define CAR_NUMBER_LENGTH 6

typedef  struct {
	int id;
	char number[CAR_NUMBER_LENGTH];
}car;

typedef  struct {
	int id;
	char* name;
	int age;
	vec* cars;
}human;


human* addHuman(int id, const char* name, int age) {
	human* h = (human*)malloc(sizeof(human));
	if (h != NULL) {
		h->id = id;
		h->name = (char*)malloc(sizeof(name));
		strcpy(h->name, name);
		h->age = age;
		h->cars = (vec*)malloc(sizeof(vec));
		if (h->cars == NULL) {
			free(h->name);
			free(h);
			return NULL;
		}
		else {
			h->cars->data = NULL;
			h->cars->next = NULL;
		}
	}
	return h;
}

human* findHuman(vec* list, int id) {
	while (list != NULL && (human*)list->data != NULL) {
		if (((human*)list->data)->id == id) {
			return (human*)list->data;
		}
		list = list->next;
	}
	return NULL;
}

void addCar(human* h, int id, const char* number) {
	car* c = (car*)malloc(sizeof(car));
	if (c != NULL) {
		c->id = id;
		strcpy(c->number, number);
		add(&h->cars, (void*)c);
	}
}

void freeMemory(vec* humans) {
	human* h;
	vec* c;
	while (humans->next != NULL)
	{
		if (humans->data != NULL) {
			h = ((human*)humans->data);
			free(h->name);
			c = h->cars;
			while (c != NULL && c->data != NULL)
			{
				free(c->data);
				c = c->next;
			}
			free(humans->data);
		}
		humans = humans->next;
	}
}

void printHuman(human* h) {
	printf("%d : %s age :%d\n", h->id, h->name, h->age);
	vec* c = h->cars;
	while (c != NULL && c->data != NULL)
	{
		printf("car id:%d, number: %s\n", ((car*)c->data)->id, ((car*)c->data)->number);
		c = c->next;
	}
}

void print(vec* humans) {
	while (humans != NULL)
	{
		if (humans->data != NULL) {

			printHuman((human*)humans->data);
		}
		humans = humans->next;
	}
}