#include "LinkedList.h"

void main()
{
	LinkedList list;

	for (int i = 0; i < 5; ++i)
	{
		list.insert(i, i * 10);
	}
	list.draw();
}