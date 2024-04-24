#pragma once
#include "init.h"

#include <iostream>
#include <stack>
#include <list>

using namespace std;

class CGameMap;

class Dijkstra
{
	CGameMap* m_gameMap;
	stack<point> m_path;		// ���� ��� ���� ����
	list<point> m_visitNode;
	bool m_bFound;

public:
	Dijkstra(CGameMap* map) : m_gameMap(map), m_bFound(false) {}
	~Dijkstra() {}

	void ExtractMin(point& choicePos, point& targetPos);
	bool FindPath(int sx, int sy, int dx, int dy);
	void Draw();
};

