#include "item.h"
#include "itemList.h"
#include <time.h>

void main()
{
	itemList itemList(20);
	
	string name = "item";
	for (int i = 0; i < 5; ++i)
	{
		item temp(rand() % 100, rand() % 100, name);
		itemList.insert(i, temp);
	}
	itemList.draw();
	cout << endl;

	item temp(rand() % 100, rand() % 100, "inserted");
	itemList.insert(1, temp);
	itemList.draw();
	cout << endl;

	itemList.remove(3);
	itemList.draw();
	cout << endl;

	item temp = itemList.retrieve(1);
}