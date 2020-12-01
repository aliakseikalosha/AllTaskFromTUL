#pragma once
#include <string>

class HTMLBlock
{
public:
	HTMLBlock();
	~HTMLBlock();
	virtual std::string print() = 0;
};

class Input :public HTMLBlock {
	std::string print() override;
};
class Password :public HTMLBlock {
	std::string print() override;

};
class Checkbox :public HTMLBlock {
	std::string print() override;

};