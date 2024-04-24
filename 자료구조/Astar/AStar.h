#pragma once
#include "init.h"
#include <stack>
#include <list>
using namespace std;

class CGameMap;

class AStar
{
	CGameMap* m_gameMap;
	stack<point> m_path;   // 최종 경로 저장 변수
	list<point> m_visitNode;
	bool m_bFound;
public:
	AStar(CGameMap* map) : m_gameMap(map), m_bFound(false) {}

	void extractMin(point& choicePos, int dx, int dy);
	bool findPath(int sx, int sy, int dx, int dy);
	void draw();
};

