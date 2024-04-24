#pragma once
#include "stack.h"

class DFS
{
	stack<int> stacks;
	int startNode;
	int targetNode;
	bool isFound;
public:
	DFS(int sn, int tn) : startNode(sn), targetNode(tn), isFound(false) {}
	~DFS() {}

	void findPath();
	void draw();
};

