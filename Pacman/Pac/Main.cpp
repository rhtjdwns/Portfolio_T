#include "MainFrame.h"

void main()
{
	MainFrame* mainFrame = new MainFrame(100);

	while (true)
	{
		mainFrame->Update();
		mainFrame->Draw();
	}
}