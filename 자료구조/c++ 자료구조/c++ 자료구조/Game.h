#pragma once
#include <iostream>
using namespace std;

class Game
{
private:
	int** map;
	int height;
	int width;
	int mine;
	
public:
	Game(int w, int h, int min) : width(w), height(h), mine(min) {
		map = new int* [h];
		for (int i = 0; i < h; ++i)
			map[i] = new int[w];
		initMap();
	}
	~Game() {
		for (int i = 0; i < height; ++i)
			delete[] map[i];
		delete[] map;
	}

	void initMap();
	int getWidth() { return width; }
	int getHeight() { return height; }
	int getElement(int x, int y) { return map[y][x]; }
	int** getMap() { return map; }
	void draw();
};

