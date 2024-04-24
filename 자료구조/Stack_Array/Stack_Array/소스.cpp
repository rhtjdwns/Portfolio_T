// #include "arrayStack.h"
#include "SimpleCalc.h"
#include <iostream>

using namespace std;

void main()
{
	//계산기 테스트
	SimpleCalc myCalc;
	char testForm[100] = "3 5 +=";
	
	myCalc.setPostfix(testForm);
	myCalc.calc();
	myCalc.draw();

	//arrayStack<int> arr;

	//// 10진수 -> 2진수
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

	//// 문자열 뒤집기

	//arrayStack<char> reverseStack;

	//char c;
	//// 키보드 입력 받으면서 푸쉬
	//while (c = cin.get(), !cin.eof())
	//{
	//	reverseStack.Push(c);
	//}
	//while (!reverseStack.IsEmpty())
	//{
	//	cout << reverseStack.Pop();
	//}

}