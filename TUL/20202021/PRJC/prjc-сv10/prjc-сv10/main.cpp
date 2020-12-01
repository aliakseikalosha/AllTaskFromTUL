#include <iostream>
#include "GeneratorHTML.h"
#include <list>
#include "User.h"
int main() {
	std::list<HTMLBlock*> page;
	User* user = new User("user", 0, 3);
	page.push_back(new Input());
	page.push_back(new Input());
	page.push_back(new Checkbox());
	page.push_back(new Password());
	page.push_back(new Password());
	std::cout << "HTML Code" << std::endl;
	for (HTMLBlock* block : page)
	{
		if (dynamic_cast<Password*>(block) && user->getUserRight() < 10) {
			std::cout << "<!--Access denied-->" << std::endl;
		}
		else {
			std::cout << block->print() << std::endl;
		}
	}

	for (HTMLBlock* block : page)
	{
		delete block;
	}
	delete user;
	return 0;
}