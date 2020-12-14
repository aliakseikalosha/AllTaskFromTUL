#pragma once
class IMoveable {
public:
	virtual void move() = 0;
};

class ISoundeable {
public:
	virtual void makeSound() = 0;
};

class Fish : public IMoveable, ISoundeable {
public:
	// Inherited via IMoveable
	virtual void move() override;

	// Inherited via ISoundeable
	virtual void makeSound() override;
};
class Bird : public IMoveable, ISoundeable {
public:
	// Inherited via IMoveable
	virtual void move() override;

	// Inherited via ISoundeable
	virtual void makeSound() override;
};
class FlyingFish : public Fish, Bird {
public:
	virtual void move() override;

	virtual void makeSound() override;
};