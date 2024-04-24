#include "Queue.h"

void Queue::enqueue(int item)
{
	//if (!isFull())
	//{
	//	queue[tail] = item;
	//	tail++;
	//	if (tail == max)
	//		tail = 0;
	//  size++
	//}
	if (!isFull())
	{
		queue[tail] = item;
		tail = (tail + 1) % max;
		size++;
	}
}

int Queue::dequeue()
{
	if (!isEmpty())
	{
		int retVal = queue[front];
		front = (front + 1) % max;
		size--;
		return retVal;
	}
	return -1;
}
