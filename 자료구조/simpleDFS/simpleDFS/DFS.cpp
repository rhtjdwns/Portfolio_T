#include "DFS.h"
#include <memory>
#include <iostream>
using namespace std;

int city[9][9] = {
	//	 a	b  c  d  e  f  g  h  i
		{0, 1, 0, 0, 0, 0, 1, 0, 0}, // a
		{0, 0, 1, 0, 1, 0, 0, 0, 0}, // b
		{0, 0, 0, 1, 0, 0, 0, 0, 0}, // c
		{0, 1, 0, 0, 0, 0, 0, 0, 0}, // d
		{0, 0, 0, 0, 0, 1, 1, 0, 0}, // e
		{0, 0, 0, 0, 0, 0, 0, 0, 0}, // f
		{0, 0, 0, 0, 0, 0, 0, 1, 0}, // g
		{0, 0, 0, 0, 0, 0, 0, 0, 0}, // h
		{0, 0, 0, 0, 0, 0, 0, 1, 0}  // i
};

enum {UNVISIT, VISIT};
char cityName[9] = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' };

void DFS::findPath()
{
	bool visitInfo[9];
	memset(visitInfo, UNVISIT, sizeof(visitInfo));

	stacks.push(startNode);
	visitInfo[startNode] = VISIT;

	while (!stacks.isEmpty() && targetNode != stacks.getTop())
	{
		int currentCity = stacks.getTop();
		bool isVisited = false;
		for (int nextCity = 0; nextCity < 9; ++nextCity)
		{
			// 인접한 방문하지 않은 노드가 있으면 방문
			if (city[currentCity][nextCity] == 1 && visitInfo[nextCity] == UNVISIT)
			{
				stacks.push(nextCity);
				visitInfo[nextCity] = VISIT;
				isVisited = true;
				break;
			}
		}
		// 방문할 노드가 없으면 백트레킹
		if (isVisited == false)
			stacks.pop();
	}
}

void DFS::draw()
{
	while (!stacks.isEmpty())
		cout << cityName[stacks.pop()] << endl;
}
