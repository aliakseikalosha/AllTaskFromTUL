#include "Editor.h"

void Color::print() {
	cout << r << " " << g << " " << b << " ";
}

Color::Color(int r_, int g_, int b_) :r(r_), g(g_), b(b_)
{
}

Color::~Color()
{
}

Color_Alfa::Color_Alfa(int r_, int g_, int b_, int a_) :Color(r_, g_, b_), a(a_)
{
}

Color_Alfa::~Color_Alfa()
{
}

void Color_Alfa::print()
{
	cout << r << " " << g << " " << b << " " << a << " ";
}

void Char::print() {
	cout << size << " " << c << " ";
	color->print();
}

Char::Char(int size_, Color* color_, char c_) : size(size_), color(color_), c(c_)
{
}

Char::~Char()
{
}

void Editor::print() {
	list<Char>::iterator i;
	for (i = chars.begin(); i != chars.end(); i++) {
		i->print();
	}
}

void Editor::addChar(Char* c)
{
	chars.push_back(*c);
}

Editor::Editor()
{
}

Editor::~Editor()
{
}
