#pragma once
#include "Plane.h"
#include "ObjectList.h"

class GameManager
{
	GameObject* plane;
	ObjectList* bulletList;
	int width;			// ∏ ¿« ∞°∑Œ≈©±‚
	int height;			// ∏ ¿« ºº∑Œ≈©±‚

	GameManager() { }
public:
	// ΩÃ±€≈Ê
	static GameManager* Instance();

	void InitGame(GameObject* _plane, ObjectList* _bullet, int w, int h) 
	{ plane = _plane, bulletList = _bullet, width = w, height = h; }

	// getter
	GameObject* getPlane() const { return plane; }
	ObjectList* getBulletList() const { return bulletList; }
	int getWidth() { return width; }
	int getHeight() { return height; }

	bool collision();
	void update();
	void draw();
};

