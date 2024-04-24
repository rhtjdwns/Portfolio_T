#pragma once
#include "Util.h"

const int Width = 27;
const int Height = 30;

class GameMap
{
public:
	GameMap();
	~GameMap();

	void Update();
	void Draw();
	int GetItem(int x, int y);

	void SetMap(int x, int y, char state) { map[y][x] = state; }
	char GetMap(int x, int y) { return map[y][x]; }
	void SetBuffer(int x, int y, char state) { back_buffer[y][x] = state; }
	int GetWidth() { return Width; }
	int GetHeight() { return Height; }

private:
	char map[Height][Width];
	char back_buffer[Height][Width];
	char front_buffer[Height][Width];
	vector<Point> point;
	vector<Point> item;
};

