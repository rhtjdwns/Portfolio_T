#include <list>
#include <iostream>

using namespace std;

struct Item
{
	int itemId;
	int buyMoney;
	Item(int c, int n)
	{
		itemId = c;
		buyMoney = n;
	}
};

int main()
{
	list<Item> ItemList;
	Item item1(1, 2000);
	ItemList.push_front(item1);
	Item item2(2, 2000);
	ItemList.push_front(item2); 
	Item item3(3, 2000);
	ItemList.push_back(item3);
	Item item4(4, 2000);
	ItemList.push_back(item4);

	list<Item>::iterator iterEnd = ItemList.end();
	for (list<Item>::iterator iterPos = ItemList.begin(); iterPos != iterEnd; ++iterPos)
		cout << "아이템 코드 : " << iterPos->itemId << endl;

	list<int> list1;
	list1.push_back(20);
	list1.push_back(30);
	cout << "삽입 테스트 1" << endl;
}