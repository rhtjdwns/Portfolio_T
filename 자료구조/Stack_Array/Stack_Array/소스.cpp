// #include "arrayStack.h"
#include "SimpleCalc.h"
#include <iostream>

using namespace std;

void main()
{
	//���� �׽�Ʈ
	SimpleCalc myCalc;
	char testForm[100] = "3 5 +=";
	
	myCalc.setPostfix(testForm);
	myCalc.calc();
	myCalc.draw();

	//arrayStack<int> arr;

	//// 10���� -> 2����
	//int num;

	//cout << "NUM : ";
	//cin >> num;
	//while (num != 0)
	//{
	//	int muk = num / 2;
	//	int nam = num % 2;
	//	arr.Push(nam);
	//	num = muk;
	//}
	//
	//while (!arr.IsEmpty())
	//{
	//	cout << arr.Pop();
	//}

	//// ���ڿ� ������

	//arrayStack<char> reverseStack;

	//char c;
	//// Ű���� �Է� �����鼭 Ǫ��
	//while (c = cin.get(), !cin.eof())
	//{
	//	reverseStack.Push(c);
	//}
	//while (!reverseStack.IsEmpty())
	//{
	//	cout << reverseStack.Pop();
	//}

}