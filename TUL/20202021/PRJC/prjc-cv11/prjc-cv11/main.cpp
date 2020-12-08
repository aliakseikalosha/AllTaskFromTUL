#include "Inheretence.h"
#include <iostream>
#include "Coposition.h"
#include <string>

void inheretenceTest() {
	std::cout << "Fish" << std::endl;
	Fish fish;
	fish.move();
	fish.makeSound();
	std::cout << "Bird" << std::endl;
	Bird bird;
	bird.move();
	bird.makeSound();
	std::cout << "FlyingFish" << std::endl;
	FlyingFish ff;
	ff.move();
	ff.makeSound();
}

void showcase(IMovement *m,IMakeSound *s, std::string message) {
	Animal a;
	a.movement = m;
	a.sound = s;
	std::cout << message << std::endl;
	a.movement->move();
	a.sound->makeSound();
	delete a.movement;
	delete a.sound;
}

void compositionTest() {
	showcase(new Swim(),new NoSound(), "Fish");
	showcase(new Fly(), new Sinning(), "Bird");
	showcase(new Fly(), new NoSound(), "FlyFish");
}

int main() {
	inheretenceTest();
	compositionTest();
	return 0;
}