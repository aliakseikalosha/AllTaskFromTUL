#include "Editor.h"
int main() {
	Editor editor;
	editor.addChar(new Char(10, new Color(255, 255, 255), 't'));
	editor.addChar(new Char(10, new Color(255, 255, 255), 'e'));
	editor.addChar(new Char(10, new Color(255, 255, 255), 's'));
	editor.addChar(new Char(10, new Color(255, 255, 255), 't'));
	editor.addChar(new Char(10, new Color(255, 255, 255), ' '));
	editor.addChar(new Char(10, new Color_Alfa(255, 255, 255,128), 'A'));
	editor.print();
	return 0;
}