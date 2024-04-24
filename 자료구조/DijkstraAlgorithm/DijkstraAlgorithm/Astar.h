#pragma once
#include "init.h"

#include <iostream>
#include <stack>
#include <list>

using namespace std;

class CGameMap;

class Astar
{
	CGameMap* m_gameMap;
	stack<point> m_path;		// 최종 경로 저장 변수
	list<point> m_visitNode;
	bool m_bFound;

public:
	Astar(CGameMap* map) : m_gameMap(map), m_bFound(false) {}
	~Astar() {}

	void ExtractMin(point& choicePos, point& targetPos);
	bool FindPath(int sx, int sy, int dx, int dy);
	void Draw();
};

