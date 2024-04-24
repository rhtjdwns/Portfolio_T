#pragma once

class CMiner;

class IState
{
public:
	virtual void Enter(CMiner* instance) = 0;
	virtual void Execute(CMiner* instance) = 0;
	virtual void Exit(CMiner* instance) = 0;
};

class IBaseGameEntity
{
public:
	virtual void Update() = 0;
};