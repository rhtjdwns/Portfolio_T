#include "util.h"

#pragma once
enum TILETYPE
{
	ZERO, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, MINE,
};
class Tile
{
	int m_x;
	int m_y;
	int m_num; // 주변에 있는 지뢰의 갯수
	bool m_isOpen; // 타일을 오픈했는지 여부
public:
	Tile() {}
	~Tile() {}
	Tile(int x, int y) : m_x(x), m_y(y), m_num(ZERO), m_isOpen(false) { }

	void intitTile(int x, int y);
	void draw();

	int getTileNum() { return m_num; }
	bool isTileOpen() { return m_isOpen; }
	int getX() { return m_x; }
	int getY() { return m_y; }

	void setTileNum(int num) { m_num = num; }
	void setTileOpen(bool bOpen) { m_isOpen = bOpen; }
	void setXY(int x, int y) { m_x = x; m_y = y; }
};

