#include "Game.h"

enum
{
	MINE = 9
};

void Game::initMap()
{
	for (int y = 0; y < height; ++y)
		for (int x = 0; x < width; ++x)
			map[y][x] = 0;

	for (int i = 0; i < mine; ++i)
	{
		int x, y;
		do
		{
			x = rand() % width;
			y = rand() % height;
		} while (map[y][x] == MINE);
		map[y][x] = MINE;

		// 지뢰 주변 값 업데이트
		for (int cy = y - 1; cy <= y + 1; ++cy)
			for (int cx = x - 1; cx <= x + 1; ++cx)
			{
				if ((cx == x && cy == y) ||
				(cx < 0 || cx > width - 1) ||
				(cy < 0 || cy > height - 1)) continue;

				if (map[cy][cx] == MINE) continue;
				map[cy][cx]++;

			}
	}
}

void Game::draw()
{
	for (int y = 0; y < height; ++y)
	{
		for (int x = 0; x < width; ++x)
			cout << map[y][x] << "   ";
		cout << endl;
	}
}
