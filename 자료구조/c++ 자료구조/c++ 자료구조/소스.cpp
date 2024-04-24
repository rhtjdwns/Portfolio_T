#include "Game.h"

using namespace std;

void main()
{
	Game* game = new Game(8, 8, 5);

	game->draw();
}