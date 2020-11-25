#pragma once
#include <iostream>
#include <list>   
using namespace std;

class Color
{
public:
	Color(int r_, int g_, int b_);
	~Color();
	virtual void print();
protected:
	int r, g, b;
};

class Color_Alfa :public Color
{
public:
	Color_Alfa(int r_, int g_, int b_, int a_);
	~Color_Alfa();
	void print() override;
protected:
	int a;
};

class Char
{
public:
	Char(int size_, Color* color_, char c_);
	~Char();
	void print();
private:
	char c;
	int size;
	Color* color;
};

class Editor
{
public:
	Editor();
	~Editor();
	void print();
	void addChar(Char* c);
private:
	list<Char> chars;
};