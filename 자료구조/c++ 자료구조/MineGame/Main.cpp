#include "util.h"
#include "Tile.h"

void main()
{
	Tile tiles[10];

	for (int i = 0; i < 10; ++i)
		tiles[i].intitTile(i, i * 2);

	ScreenInit();
	ScreenClear();

	bool isContinue = true;
	while (isContinue)
	{
		bool bBomb = false;
		bool isWin = false;
		int openTileNum;

		int nKey;
		int cursorX;
		int cursorY;

		for (int i = 0; i < 10; ++i)
		{
			tiles[i].setTileNum(i);
			tiles[i].setTileOpen(true);
			tiles[i].draw();
		}

		ScreenFlipping();
	}
}