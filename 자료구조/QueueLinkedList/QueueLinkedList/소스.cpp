#include "queue.h"
#include <iostream>

using namespace std;

int main()
{
	queue qu;

	for (int i = 0; i < 3; ++i)
		qu.enQueue(1 * i);

	for (int i = 0; i < 3; ++i)
		cout << qu.deQueue() << endl;

	return 0;
}