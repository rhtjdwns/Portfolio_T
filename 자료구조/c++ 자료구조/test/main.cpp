#include "util.h"

void DrawTest()
{

}

void main()
{
	ScreenInit();

	// ���� ����
	// 1. ȭ�� Ŭ����
	// 2. �޸𸮿� ȭ�� ��� ������ ����
	// 3. �޸𸮹��� Ȱ��ȭ => ���� ���۸�
	// 4. �Է�ó��
	// 5. ������Ʈ

	ScreenClear();
	while (1)
	{
		SetColor(15);
		ScreenPrint(5, 5, "A");
		ScreenPrint(10, 5, "B");

		SetColor(10);
		DrawTest();

		ScreenFlipping();
	}
	ScreenRelease();
}