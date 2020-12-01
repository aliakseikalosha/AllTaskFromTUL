#include "User.h"

std::string User::getUserName()
{
	return user_name;
}

void User::changeUserName(std::string newName)
{
	user_name = newName;
}

int User::getUserRight()
{
	return user_rights;
}

void User::setUserRight(int newRight)
{
	user_rights = std::min(10, std::max(0, newRight));
}

int User::getContractType()
{
	return user_contract_type;
}

void User::setContractType(int newContract)
{
	user_contract_type = std::min(5, std::max(0, newContract));
}

User::User(std::string uName, int uRight, int uContractType) :user_name(uName)
{
	user_rights = std::min(10, std::max(0, uRight));
	user_contract_type = std::min(5, std::max(0, uContractType));
}
