using UnityEngine;

public class StateGameplay : State 
{
	private string _levelName;
	private LevelMap _levelMap;
	private GameplayHUD _gameHUD;

	public StateGameplay(string levelName)
	{
		_levelName = levelName;
	}

	#region overriding from State
	public override void Enter()
	{
		ShowMouse(false);

		GameObject gameHUDObject = UIManager.GetInstance().InstantiateForegroundUI(UIManager.GetInstance().GameplayHUD);
		_gameHUD = gameHUDObject.GetComponent<GameplayHUD>();
		_gameHUD.Init();

		_levelMap = LevelObjectManager.GetInstance().InstantiateLevel(_levelName);
		_levelMap.Player.Init(_gameHUD);
		_levelMap.Player.PlayerDead += OnPlayerDead;
		_levelMap.Player.KilledEnemy += OnEnemyDead;
	}
	
	public override void Exit()
	{
		_levelMap.Player.enabled = false;
		GameObject.Destroy(_levelMap.gameObject);
		/* We must delay HUD destruction a bit, since we cannot destroy NGUI elements mid-frame whilst physics triggers/functions are active;
		   this is because NGUI uses DestroyImmediate when in Editor and Unity will not allow physics calls to occur after DestroyImmediate in that frame */
		GameObject.Destroy(_gameHUD.gameObject, .001f);	

		ShowMouse(true);
	}
	
	public override void Update(float deltaTime)
	{}
	#endregion

	#region private
	private void OnPlayerDead()
	{
		OnGameOver();
	}

	private void OnEnemyDead(Enemy enemy)
	{
		GameObject pickup = ItemPickupManager.GetInstance().InstantiateRandomPickup(enemy.transform.position);
		pickup.transform.parent = _levelMap.transform;
	}

	private void OnGameOver()
	{
		_stateMachine.SetState(new StateGameOver(_levelName));
	}
	#endregion
}
