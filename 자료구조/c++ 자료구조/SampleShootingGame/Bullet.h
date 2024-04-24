#pragma once
#include "GameObject.h"
class Bullet : public GameObject
{
public:
	Bullet(int x, int y, const char* img) : GameObject(x, y, img) { }

	void update() override;
};

