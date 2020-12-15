#pragma once
#include <string>

class IFileSave {
public:
 virtual void save(std::string name,std::string data) = 0;
};

class PDFFileSave : public IFileSave {
	// Inherited via IFileSave
	virtual void save(std::string name, std::string data) override;
};