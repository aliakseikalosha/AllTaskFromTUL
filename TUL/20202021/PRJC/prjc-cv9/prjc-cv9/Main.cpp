#include "Editor.h"
int main() {
	Editor* editor = new Editor();
	Color* white = new Color(255, 255, 255);
	Color_Alfa* whiteT = new Color_Alfa(255, 255, 255, 128);
	Char* t = new Char(10, white, 't');
	Char* e = new Char(10, white, 'e');
	Char* s = new Char(10, white, 's');
	Char* space = new Char(10, white, ' ');
	Char* A = new Char(10, whiteT, 'A');
	editor->addChar(t);
	editor->addChar(e);
	editor->addChar(s);
	editor->addChar(t);
	editor->addChar(space);
	editor->addChar(A);
	editor->print();
	delete A;
	delete t;
	delete e;
	delete s;
	delete space;
	delete white;
	delete whiteT;
	delete editor;
	return 0;
}