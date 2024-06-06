#include "FSM.h"

extern InBox* inbox;
extern Hunter* hunter;
extern Hunted* hunted;
extern Eaten* eaten;

void InBox::Enter(Ghost* instance)
{
	startTime = clock();
}

void InBox::Execute(Ghost* instance)
{
	endTime = clock();
	if (endTime - startTime >= 5)
	{
		instance->ChangeState(hunter);
	}
}

void InBox::Exit(Ghost* instance)
{
}

void Hunter::Enter(Ghost* instance)
{
}

void Hunter::Execute(Ghost* instance)
{
}

void Hunter::Exit(Ghost* instance)
{
}

void Hunted::Enter(Ghost* instance)
{
}

void Hunted::Execute(Ghost* instance)
{
}

void Hunted::Exit(Ghost* instance)
{
}

void Eaten::Enter(Ghost* instance)
{
}

void Eaten::Execute(Ghost* instance)
{
}

void Eaten::Exit(Ghost* instance)
{
}
