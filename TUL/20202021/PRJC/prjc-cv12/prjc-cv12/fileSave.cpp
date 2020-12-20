#define _CRT_SECURE_NO_WARNINGS
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include "fileSave.h"
#include <windows.h>
#include <iostream>
#include <stdio.h>
#include <string.h>

#include "share/litePDF.h"

void PDFFileSave::save(std::string name, std::string data)
{
	TLitePDF litePDF;

	// begin write-only PDF file
	litePDF.CreateFileDocument((name + ".pdf").c_str());

	// add a new page to it, with large-enough pixel scale
	HDC hDC = litePDF.AddPage(litePDF.MMToUnit(210), litePDF.MMToUnit(297), 2100, 2970, LitePDFDrawFlag_None);

	// draw the text
	LOGFONTA lf = { 0, };
	lf.lfHeight = -30; // ~1/10 of the page height
	strcpy(lf.lfFaceName, "Arial");

	HFONT fnt;
	HGDIOBJ oldFnt;

	fnt = CreateFontIndirect(&lf);
	oldFnt = SelectObject(hDC, fnt);

	SetTextColor(hDC, RGB(0, 0, 0));

	size_t pos = 0;
	int i = 0;
	std::string token;
	std::string delimiter = "\n";
	while ((pos = data.find(delimiter)) != std::string::npos) {
		i++;
		token = data.substr(0, pos);
		data.erase(0, pos + delimiter.length());
		TextOut(hDC, 8, 8 + 24 * i, token.c_str(), token.length() - 1);
	}
	std::cout << data.c_str() << std::endl;
	TextOut(hDC, 8, 8, data.c_str(), data.size());

	SelectObject(hDC, oldFnt);
	DeleteObject(fnt);

	// finish drawing
	litePDF.FinishPage(hDC);

	// close the document
	litePDF.Close();
}