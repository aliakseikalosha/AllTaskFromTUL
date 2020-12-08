#include "Coposition.h"
#include <iostream>

void Fly::move()
{
	std::cout << "Fly" << std::endl;
}

void Swim::move()
{
	std::cout << "Swim" << std::endl;
}

void NoSound::makeSound()
{
	std::cout << "NoSound" << std::endl;
}

void Sinning::makeSound()
{
	std::cout << "Sinning" << std::endl;
}
