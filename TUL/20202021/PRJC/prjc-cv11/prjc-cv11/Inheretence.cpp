#include "Inheretence.h"
#include <iostream>

void Fish::move()
{
	std::cout << "swim" << std::endl;
}

void Fish::makeSound()
{
	std::cout << "NO SOUND SUPPORTED" << std::endl;
}

void Bird::move()
{
	std::cout<< "Fly" << std::endl;
}

void Bird::makeSound()
{
	std::cout<<"Bird sinning."<<std::endl;
}

void FlyingFish::move()
{
	this->Bird::move();
}

void FlyingFish::makeSound()
{
	this->Fish::makeSound();
}
