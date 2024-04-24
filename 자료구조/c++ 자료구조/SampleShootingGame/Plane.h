#pragma once
#include "GameObject.h"

class Plane : public GameObject
{
public:
	Plane(int x, int y, const char* img) : GameObject(x, y, img) { }
	~Plane() { }

	void update() override;
};

