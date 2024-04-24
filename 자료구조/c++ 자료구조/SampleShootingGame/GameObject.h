#pragma once
#include "util.h"

class GameObject
{
protected:
	int x, y;
	const char* image;		// game image
public:
	GameObject() : x(0), y(0), image(" ") { }
	~GameObject() { }
	GameObject (int X, int Y, const char* IMAGE) : x(X), y(Y), image(IMAGE) { }

	// getter
	int getX() { return x; }
	int getY() { return y; }
	const char* getImage() { return image; }

	// setter
	void setXY(int X, int Y) { x = X; y = Y; }
	void setImg(const char* IMAGE) { image = IMAGE; }

	virtual void update() { }

	bool collision(GameObject* other)
	{
		if (this->x == other->getX() && this->y == other->getY())
			return true;
		return false;
	}
	void draw()
	{
		ScreenPrint(x, y, image);
	}
};

