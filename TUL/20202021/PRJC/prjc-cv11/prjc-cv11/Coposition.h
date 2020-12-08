#pragma once


class IMovement {
public:
	 void virtual move() = 0;
};
class IMakeSound {
public:
	void virtual makeSound() = 0;
};

class Fly:public IMovement {
	// Inherited via IMovement
	virtual void move() override;
};
class Swim :public IMovement {
	// Inherited via IMovement
	virtual void move() override;
};

class NoSound :public IMakeSound {
	// Inherited via IMakeSound
	virtual void makeSound() override;
};
class Sinning :public IMakeSound {
	// Inherited via IMakeSound
	virtual void makeSound() override;
};
class Animal {
public:
	IMakeSound* sound;
	IMovement* movement;
};