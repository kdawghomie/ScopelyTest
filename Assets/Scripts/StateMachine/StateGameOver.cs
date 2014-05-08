using UnityEngine;

public class StateGameOver : State 
{
	private GameOverUI _gameOverUI;
	private string _levelName;
	private int _score;

	public StateGameOver(string levelName, int score)
	{
		_levelName = levelName;
		_score = score;
	}
	
	public override void Enter()
	{
		GameObject gameOverObject = UIManager.GetInstance().InstantiateForegroundUI(UIManager.GetInstance().GameOverUI);
		_gameOverUI = gameOverObject.GetComponent<GameOverUI>();
		_gameOverUI.Init(_score);
		_gameOverUI.MainMenuPressed += OnMainMenuPressed;
		_gameOverUI.RetryPressed += OnRetryPressed;
	}
	
	public override void Exit()
	{
		GameObject.Destroy(_gameOverUI.gameObject);
	}

	public override void Update(float deltaTime)
	{}

	private void OnMainMenuPressed()
	{
		_stateMachine.SetState(new StateGameStart());
	}
	
	private void OnRetryPressed()
	{
		_stateMachine.SetState(new StateGameplay(_levelName));
	}
}
