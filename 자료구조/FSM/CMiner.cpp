#include "CMiner.h"
#include <thread>

void CMiner::Update()
{
	currentState->Execute(this);
}

void CMiner::ChangeState(IState* pNewState)
{
	if(currentState != nullptr)
		currentState->Exit(this);
	currentState = pNewState;
	currentState->Enter(this);
}
