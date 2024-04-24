#pragma once
class Queue
{
	int* queue;
	int front;
	int tail;
	int max;
	int size;
public:
	Queue(int size = 100) : front(0), tail(0), size(0), max(size) {
		queue = new int[size];
	}
	~Queue() {
		if (queue)
			delete queue;
	}
	void enqueue(int item);
	int dequeue();
	bool isEmpty() { return (size == 0); }
	bool isFull() { return (size == max); }
	int getFront() { return isEmpty() ? -1 : queue[front]; }
	int getSize() { return size; }
};

