#pragma once

class GameObject;

struct node
{
	GameObject* object;
	node* next;
};

class ObjectList
{
	node* head;
	int count;
public:
	ObjectList() : head(nullptr), count(0) { }
	~ObjectList();

	void insert(int pos, GameObject* obj);
	void append(GameObject* obj) { insert(count, obj); }
	void remove(int pos);
	void remove(GameObject* obj);
	GameObject* getObject(int pos);
	int getPos(GameObject* obj);
	int getSize() const { return count; }

	void update();
	void draw();
};

