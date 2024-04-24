#include "GameMap.h"
#include "Dijkstra.h"
#include "Astar.h"

int main()
{
	CGameMap gameMap(10, 10);
	//Dijkstra shortestPath(&gameMap);
	Astar shortestPath(&gameMap);

	shortestPath.Draw();
	shortestPath.FindPath(0, 9, 9, 0);
	shortestPath.Draw();

	return 0;
}