using UnityEngine;

public class StateHowToPlay : State 
{
	private HowToPlayUI _howToPlayUI = null;
	
	public StateHowToPlay()
	{}
	
	public override void Enter()
	{
		GameObject howToPlayObject = UIManager.GetInstance().InstantiateForegroundUI(UIManager.GetInstance().HowToPlayUI);
		_howToPlayUI = howToPlayObject.GetComponent<HowToPlayUI>();
		_howToPlayUI.MainMenuPressed += OnMainMenuPressed;
	}
	
	public override void Exit()
	{
		GameObject.Destroy(_howToPlayUI.gameObject);
	}

	public override void Update(float deltaTime)
	{}

	private void OnMainMenuPressed()
	{
		_stateMachine.SetState(new StateGameStart());
	}
}
