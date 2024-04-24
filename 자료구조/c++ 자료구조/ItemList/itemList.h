#pragma once
class item;

class itemList
{
	item* m_items;
	int m_count;	// 리스트에
	int m_maxSize;
public:
	itemList();
	~itemList();
	itemList(int Max);

	void insert(int pos, item item);
	void remove(int pos);
	item retrieve(int pos);
	bool isEmpty() { return m_count == 0; }
	bool isFull() { return; }
	void draw();
};

