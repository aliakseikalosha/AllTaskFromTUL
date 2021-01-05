#include <iostream>

#define _CRT_SECURE_NO_WARNINGS
#define _WINSOCK_DEPRECATED_NO_WARNINGS

class IItem
{
public:
	virtual int Attack() = 0;
	virtual int Defence() = 0;
};

class IAttackItem : public IItem {
private:
	virtual int Defence() override
	{
		return 0;
	}
};

class IDefenceItem : public IItem {
private:
	// Inherited via IItem
	virtual int Attack() override
	{
		return 0;
	}
};
class Shield :public IDefenceItem {
	// Inherited via IDefenceItem
	virtual int Defence() override
	{
		return 10;
	}
};

class Sword : public IAttackItem {
	// Inherited via IAttackItem
	virtual int Attack() override
	{
		return 5;
	}
};

class NPC
{
public:
	IDefenceItem* head;
	IDefenceItem* body;
	IItem* armL;
	IItem* armR;

	int Atack() {
		int a = 0;
		if (armL != NULL) {
			a += armL->Attack();
		}
		if (armR != NULL) {
			a += armR->Attack();
		}
		return a;
	}

	int Defence() {

		int d = 0;
		if (armL != NULL) {
			d += armL->Defence();
		}
		if (armR != NULL) {
			d += armR->Defence();
		}
		if (head != NULL) {
			d += head->Defence();
		}
		if (body != NULL) {
			d += body->Defence();
		}
		return d;
	}

	bool Fight(NPC* other) {
		return other->Defence() > this->Atack();
	}

	NPC() {

	}
	~NPC() {
		if (armL != NULL) {
			delete armL;
		}
		if (armR != NULL) {
			delete armR;
		}
		if (head != NULL) {
			delete head;
		}
		if (body != NULL) {
			delete body;
		}
	}

};

int main() {
	NPC* n1 = new NPC();
	n1->head = new Shield();
	n1->armL = new Shield();
	n1->armR = new Sword();
	n1->body = new Shield();
	NPC* n2 = new NPC();
	n2->armL = new Sword();
	n2->armR = new Sword();
	std::cout << "N1 wins N2 ? " << (n2->Fight(n1) ? " Yes " : " No ") << std::endl;
	delete n1;
	delete n2;
	std::cout << " All deleted" << std::endl;
}
