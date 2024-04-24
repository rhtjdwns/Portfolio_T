#include "queue.h"


void queue::enQueue(int item)
{
	Node* inNode = new Node;
	inNode->data = item;
	inNode->next = nullptr;

	if (!tail)
	{
		tail = inNode;
		front = inNode;
	}
	else
	{
		tail->next = inNode;
		tail = inNode;
	}
	size++;
}

int queue::deQueue()
{
	Node* delNode;
	int temp;

	if (size == 0)
		return -1;

	temp = front->data;
	delNode = front;
	front = front->next;
	if (size == 1)
		tail = nullptr;

	delete delNode;
	size--;
	return temp;
}

