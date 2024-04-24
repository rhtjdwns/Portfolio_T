#pragma once

template <class T>
struct node
{
	node* next;
	T item;
};

template <class T>
class stack
{
private:
	node<T>* top;
public:
	stack() { top = nullptr; }
	~stack();
	void push(T Item);
	T pop();
	T getTop();
	bool isEmpty();
};

#include "stack.inl"