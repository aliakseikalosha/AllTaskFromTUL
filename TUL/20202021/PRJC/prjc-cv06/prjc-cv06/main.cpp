#define _CRT_SECURE_NO_WARNINGS
#include "basicVec.h"
#include "dataType.h"


int main() {
	vec peoples;
	peoples.data = NULL;
	peoples.next = NULL;
	int i = 1;
	vec* p_peoples = &peoples;
	add(&p_peoples, (void*)addHuman(i++, "Jan", 20));
	add(&p_peoples, (void*)addHuman(i++, "Dana", 20));
	add(&p_peoples, (void*)addHuman(i++, "Kate", 20));
	add(&p_peoples, (void*)addHuman(i++, "Petr", 20));
	i = 0;
	addCar(findHuman(p_peoples, 1), i++, "123456");
	addCar(findHuman(p_peoples, 1), i++, "12dasd");
	addCar(findHuman(p_peoples, 2), i++, "123ae6");
	addCar(findHuman(p_peoples, 1), i++, "zadfa6");
	addCar(findHuman(p_peoples, 4), i++, "zadfa6");
	addCar(findHuman(p_peoples, 4), i++, "zadfa6");
	print(p_peoples);
	freeMemory(p_peoples);
}