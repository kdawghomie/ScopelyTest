using System;

public abstract class StateMachine
{
	protected State _currentState = null;

	public virtual void SetState(State state)
	{
		if(_currentState != null)
		{
			_currentState.Exit();
		}
		_currentState = state;
		_currentState.Enter();
	}

	public virtual void Update(float deltaTime) 
	{
		if(_currentState != null)
		{
			_currentState.Update(deltaTime);
		}
	}

	public virtual void DestroyStateMachine()
	{
		if(_currentState != null)
		{
			_currentState.Exit();
		}
	}
}
