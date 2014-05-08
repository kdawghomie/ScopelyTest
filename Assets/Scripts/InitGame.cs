using UnityEngine;
using System.Collections;

public class InitGame : MonoBehaviour
{
	private GameStateMachine _stateMachine = null;

	void Awake()
	{
		_stateMachine = new GameStateMachine();
		_stateMachine.SetState(new StateGameStart());
	}

	void Update()
	{
		if(_stateMachine != null)
		{
			_stateMachine.Update(Time.deltaTime);
		}
	}
}
