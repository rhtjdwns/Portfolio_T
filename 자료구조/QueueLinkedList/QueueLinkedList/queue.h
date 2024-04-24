#pragma once
struct Node
{
	int data;
	Node* next;
};

class queue
{
	Node* front;
	Node* tail;
	int size;
public:
	queue() : size(0)
	{
		front = nullptr;
		tail = nullptr;
	}
	~queue() { }

	void enQueue(int item);
	int deQueue();
	int getSize() { return size; }
	int getFront() { return front != nullptr ? front->data : -1; }
	bool isEmpty() { return (size == 0); }
};

