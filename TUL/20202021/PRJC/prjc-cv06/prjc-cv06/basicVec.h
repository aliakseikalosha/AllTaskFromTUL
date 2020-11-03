#pragma once
#include <cstddef>
#include <malloc.h>

typedef struct vec {
	void* data;
	vec* next;
}vec;

int getLength(vec* v) {
	int i = 1;
	while (v->next != NULL) {
		i++;
		v = v->next;
	}
	return i;
}

vec* getLast(vec* v) {
	if (v == NULL) {
		return NULL;
	}
	while (v->next != NULL)
	{
		v = v->next;
	}
	return v;
}

void add(vec** p_v, void* data) {
	vec* v = (vec*)malloc(sizeof(vec));
	if (v != NULL) {
		v->data = data;
		v->next = NULL;
		if (*p_v == NULL) {
			p_v = &v;
		}
		else {
			vec* d = getLast((*p_v));
			if (d->data == NULL)
			{
				d->data = data;
				free(v);
			}
			else
			{
				d->next = v;
			}
		}
	}
}