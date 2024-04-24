#pragma once
#include <iostream>

using namespace std;

class Tree
{
public:
	Tree(int d) { data = d, left = nullptr, right = nullptr; }
	~Tree() {}

	Tree* Search(Tree* T, int key);
	Tree* Insert(Tree* T, int key);
	Tree* Delete(Tree* T, int key);

	int GetData();
private:
	int data;
	Tree* left;
	Tree* right;
};

int Tree::GetData()
{
	return this->data;
}

Tree* Tree::Search(Tree* T, int key)
{
	if (T == NULL)
		return NULL;
	else if (T->data == key)
		return T;
	else if (T->data > key)
		return Search(T->left, key);
	else
		return Search(T->right, key);
}

Tree* Tree::Insert(Tree* T, int key)
{
	if (T == NULL)
		T = new Tree(key);
	else if (T->data > key)
		T->left = Insert(T->left, key);
	else if (T->data < key)
		T->right = Insert(T->right, key);
	return T;
}

Tree* Tree::Delete(Tree* T, int key)
{
	if (T == NULL)
		return NULL;
	else if (T->data > key)
	{
		T->left = Delete(T->left, key);
		return T;
	}
	else if (T->data < key)
	{
		T->right = Delete(T->right, key);
		return T;
	}
	else if (T->data == key)
	{
		if (T->left == NULL && T->right == NULL)
		{
			delete T;
			return NULL;
		}
		else if (T->left != NULL)
		{
			Tree* temp = T->left;
			delete T;
			return temp;
		}
		else if (T->right != NULL)
		{
			Tree* temp = T->right;
			delete T;
			return temp;
		}
		else
		{
			Tree* node = T->left;
			while (node->right != nullptr)
				node = node->right;
			T->data = node->data;
			T->left = Delete(node, node->data);
			return T;
		}
	}
	return T;
}
