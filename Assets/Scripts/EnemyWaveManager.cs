using UnityEngine;

public class EnemyWaveManager
{
	private readonly int _minNextWaveEnemyIncrease;
	private readonly int _maxNextWaveEnemyIncrease;
	private int _waveEnemyCount;
	private int _enemiesSpawnedInWave = 0;

	public EnemyWaveManager(int waveEnemyCount, int minNextWaveEnemyIncrease, int maxNextWaveEnemyIncrease)
	{
		_waveEnemyCount = waveEnemyCount;
		_minNextWaveEnemyIncrease = minNextWaveEnemyIncrease;
		_maxNextWaveEnemyIncrease = maxNextWaveEnemyIncrease;
	}

	public void OnEnemySpawned()
	{
		_enemiesSpawnedInWave++;
	}

	public bool IsWaveComplete()
	{
		return _enemiesSpawnedInWave >= _waveEnemyCount;
	}

	public void BeginNextWave()
	{
		_enemiesSpawnedInWave = 0;
		int numberAdditionalEnemies = Random.Range(_minNextWaveEnemyIncrease, _maxNextWaveEnemyIncrease+1);
		_waveEnemyCount += numberAdditionalEnemies;
	}
}
