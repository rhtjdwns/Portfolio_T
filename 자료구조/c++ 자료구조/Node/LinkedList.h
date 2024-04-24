#pragma once

struct node {
	int data;
	node* next;
};

class LinkedList
{
	node* head;
	int count;
public:
	LinkedList() : head(nullptr), count(0) { }
	~LinkedList() { }

	void insert(int pos, int data);
	void remove(int pos);
	int retrieve(int pos);
	bool isEmpty() { return count == 0; }
	void draw();
};

