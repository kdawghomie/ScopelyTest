using UnityEngine;

public class StateGameStart : State 
{
	private StartScreenUI _startScreenUI = null;

	public StateGameStart()
	{}
	
	public override void Enter()
	{
		GameObject startScreenObject = UIManager.GetInstance().InstantiateForegroundUI(UIManager.GetInstance().StartScreenUI);
		_startScreenUI = startScreenObject.GetComponent<StartScreenUI>();
		_startScreenUI.PlayPressed += OnPlayButtonPressed;
	}
	
	public override void Exit()
	{
		GameObject.Destroy(_startScreenUI.gameObject);
	}
	
	public override void Update(float deltaTime)
	{
		
	}

	private void OnPlayButtonPressed()
	{
		_stateMachine.SetState(new StateGameplay("level1"));
	}
}
