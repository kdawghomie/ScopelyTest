using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	public const float SPAWN_COOLDOWN = 1.0f;
	public const float SPAWN_RADIUS = 128.0f;
	
	private float _cooldown = 0.0f;

	public GameObject zunny;

	private EnemyWaveManager _enemyWaveManager = null;
	private bool _waveComplete = false;

	#region Properties
	public bool WaveComplete
	{
		set{ _waveComplete = value; }
	}
	#endregion

	#region Unity Lifecycle
	public void Init(EnemyWaveManager enemyWaveManager)
	{
		_cooldown = Random.value * 3.0f;
		_enemyWaveManager = enemyWaveManager;
	}

	void Update () 
	{
		if(!_enemyWaveManager.IsWaveComplete())
		{
			_cooldown -= Time.deltaTime;
			if(_cooldown < 0.0f)
			{
				_cooldown = SPAWN_COOLDOWN;
				SpawnEnemy();
				_enemyWaveManager.OnEnemySpawned();
			}
		}
	}
	#endregion

	#region Helpers
	private void SpawnEnemy()
	{
		GameObject e = GameObject.Instantiate(zunny) as GameObject;
		float radius = Mathf.Sqrt(Random.value * SPAWN_RADIUS);
		float rot = Random.value * Mathf.PI * 2.0f;
		Vector3 spawnPos = new Vector3(Mathf.Sin(rot) * radius, Mathf.Cos(rot) * radius, 0.0f) + this.transform.position;
		e.transform.parent = this.transform.parent;
		e.transform.position = spawnPos;
	}
	public int EnemyCount(){
		Enemy[] enemies = (Enemy[])GameObject.FindObjectsOfType(typeof(Enemy));
		return enemies.Length;
	}
	#endregion
}
