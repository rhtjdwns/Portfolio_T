#pragma once
#include "Tile.h"

class GameMap
{
	Tile** m_map;
	int m_width;		// 맵의 가로크기
	int m_height;		// 맵의 세로크기
	int m_openTileNum;	// 오픈한 타일의 갯수
	int m_mineNum;		// 지뢰의 갯수
public:
	GameMap() {}
	~GameMap() {}
	GameMap(int w, int h) : m_width(w), m_height(h), m_openTileNum(0), m_mineNum(0) {
		m_map = new Tile * [h];
		for (int i = 0; i < h; ++i)
			m_map[i] = new Tile[w];

		for (int y = 0; y < h; ++y)
			for (int x = 0; x < w; ++x)
				m_map[y][x].intitTile(x, y);
	}

	int getWidth() { return m_width; }
	int getHeight() { return m_height; }
	int getMineNum() { return m_mineNum; }

	// 지뢰설치 함수
	void setMine(int mineNum);
	void updateTile(int x, int y);

	int getTileNum(int x, int y) { return m_map[y][x].getTileNum(); }
	int openTile(int x, int y);		// x, y 위치의 타일을 오픈
	void openAllTile();				// 모든 타일 오픈

	void draw();
};

