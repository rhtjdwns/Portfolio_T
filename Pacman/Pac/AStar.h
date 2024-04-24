#pragma once
#include "GameMap.h"
#include <stack>
#include <list>

using namespace std;

class AStar
{
	GameMap* m_gameMap;
	stack<Point> m_path;		// ���� ��� ���� ����
	list<Point> m_visitNode;
	int** m_map;
	bool** m_visitInfo;
	bool m_bFound;

public:
	AStar(GameMap* map) : m_gameMap(map), m_bFound(false) 
	{
		m_map = new int* [Height];
		m_visitInfo = new bool* [Height];
		for (int i = 0; i < Height; ++i)
		{
			m_map[i] = new int[Width];
			m_visitInfo[i] = new bool[Width];
		}
	}
	~AStar() {}

	void ExtractMin(Point& choicePos, Point& targetPos);
	stack<Point> FindPath(int sx, int sy, int dx, int dy);
};