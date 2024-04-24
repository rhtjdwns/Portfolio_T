#include "Queue.h"
#include <iostream>

using namespace std;

int main()
{
	Queue queueList(5);

	for (int i = 0; i < 7; ++i)
		queueList.enqueue(i * 10);

	cout << queueList.getSize() << endl;
	cout << queueList.dequeue() << endl;
	cout << queueList.dequeue() << endl;
	cout << queueList.dequeue() << endl;
	cout << queueList.dequeue() << endl;
	cout << queueList.dequeue() << endl;
	cout << queueList.dequeue() << endl;
	return 0;
}