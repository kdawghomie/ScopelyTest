using System;

public abstract class State 
{
	protected StateMachine _stateMachine;

	public StateMachine StateMachine
	{
		set{ _stateMachine = value; }
	}

	public abstract void Enter();

	public abstract void Exit();

	public abstract void Update(float deltaTime);
}
