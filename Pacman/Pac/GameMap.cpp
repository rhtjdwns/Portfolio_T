#include "GameMap.h"

GameMap::GameMap()
{
	char copyMap[Height][Width + 1] = {
		"###########################" ,
		"#............#............#" ,
		"#.####.#####.#.#####.####.#" ,
		"#*####.#####.#.#####.####*#" ,
		"#.####.#####.#.#####.####.#" ,
		"#.........................#" ,
		"#.####.##.#######.##.####.#" ,
		"#.####.##.#######.##.####.#" ,
		"#......##....#....##......#" ,
		"######.##### # #####.######" ,
		"     #.##### # #####.#     " ,
		"     #.##         ##.#     " ,
		"     #.## ##   ## ##.#     " ,
		"######.## #     # ##.######" ,
		"      .   #     #   .      " ,
		"######.## #     # ##.######" ,
		"     #.## ####### ##.#     " ,
		"     #.##         ##.#     " ,
		"     #.## ####### ##.#     " ,
		"######.## ####### ##.######" ,
		"#............#............#" ,
		"#.####.#####.#.#####.####.#" ,
		"#.####.#####.#.#####.####.#" ,
		"#*..##....... .......##..*#" ,
		"###.##.##.#######.##.##.###" ,
		"###.##.##....#....##.##.###" ,
		"#......#####.#.#####......#" ,
		"#.##########.#.##########.#" ,
		"#.........................#" ,
		"###########################" ,
	};

	Point p;

	for (int i = 0; i < Height; i++) 
	{
		for (int j = 0; j < Width; j++) 
		{
			switch (copyMap[i][j])
			{
				case ' ': 
					map[i][j] = SPACE; 
					break;
				case '#': 
					map[i][j] = WALL; 
					break;
				case '.': 
					p.x = j; p.y = i;
					point.push_back(p); map[i][j] = SPACE;
					break;
				case '*': 
					p.x = j; p.y = i; item.push_back(p);
					map[i][j] = SPACE; 
					break;
			}
			back_buffer[i][j] = map[i][j];
		}
	}
}

GameMap::~GameMap()
{

}

void GameMap::Update()
{
	for (vector<Point>::iterator iter = point.begin(); iter != point.end(); iter++)
		back_buffer[iter->y][iter->x] = p_POINT;
	for (vector<Point>::iterator iter = item.begin(); iter != item.end(); iter++)
		back_buffer[iter->y][iter->x] = ITEM;
}

void GameMap::Draw()
{
	for (int i = 0; i < Height; i++) 
	{
		for (int j = 0; j < Width; j++) 
		{
			if (back_buffer[i][j] != front_buffer[i][j]) 
			{
				gotoxy(j * 2, i);
				switch (back_buffer[i][j]) 
				{
					case p_POINT:	
						SetColor(DARKYELLOW, BLACK); 
						cout << ". ";
						break;
					case ITEM:  
						SetColor(DARKYELLOW, BLACK); 
						cout << "б┘"; 
						break;
					case SPACE: 
						SetColor(WHITE, BLACK); 
						cout << "  "; 
						break;
					case WALL:  
						SetColor(BLUE, BLACK);  
						cout << "бс"; 
						break;
					case PLAYER:
						SetColor(YELLOW, BLACK); 
						cout << "б▄"; 
						break;
					case ENEMY:
						SetColor(RED, BLACK);	
						cout << "бу"; 
						break;
				}
			}
			front_buffer[i][j] = back_buffer[i][j];
			back_buffer[i][j] = map[i][j];
		}
	}
}

int GameMap::GetItem(int x, int y)
{
	for (vector<Point>::iterator iter = point.begin(); iter != point.end(); ++iter)
	{
		if (x == iter->x && y == iter->y)
		{
			point.erase(iter);
			return p_POINT;
		}
	}
	for (vector<Point>::iterator iter = item.begin(); iter != item.end(); ++iter)
	{
		if (x == iter->x && y == iter->y)
		{
			item.erase(iter);
			return ITEM;
		}
	}

	return SPACE;
}
