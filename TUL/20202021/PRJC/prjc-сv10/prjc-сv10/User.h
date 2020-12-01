#pragma once
#include <string>
class User {

public:
	std::string getUserName();
	void changeUserName(std::string newName);

	int getUserRight();
	void setUserRight(int newRight);
	int getContractType();
	void setContractType(int newContract);
	User(std::string uName, int uRight, int uContractType);

private:

	std::string user_name;

	int user_rights; // int within range <0 - 10>

	int user_contract_type; // int within range <0 - 5>

};