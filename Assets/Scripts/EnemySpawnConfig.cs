using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnConfig : MonoBehaviour 
{
	// can't serialize dictionaries so using 2 lists in parallel
	[SerializeField] private List<GameObject> _enemyTypes;
	[SerializeField] private List<float> _enemyTypeSpawnOdds; // total should add up to 1.0

	private void Awake()
	{
		if(_enemyTypes.Count != _enemyTypeSpawnOdds.Count)
		{
			Debug.LogError("EnemySpawnConfig: mismatch between number of enemies and number of enemy spawn odds");
			return;
		}

		float totalSpawnOdds = 0f;
		foreach(float spawnOdds in _enemyTypeSpawnOdds)
		{
			totalSpawnOdds += spawnOdds;
		}
		if(totalSpawnOdds != 1.0f)
		{
			Debug.LogError("EnemySpawnConfig: total enemy spawn odds does not add up to 1.0");
			return;
		}
	}

	public GameObject GetRandomEnemyPrefabForSpawn()
	{
		GameObject result = null;

		float randomFloat = Random.Range(0.0f, 1.0f);
		float percentageLowerBound = 0.0f;
		float percentageUpperBound = 0.0f;
		for(int i = 0; i < _enemyTypeSpawnOdds.Count; i++)
		{
			float spawnOdds = _enemyTypeSpawnOdds[i];
			percentageUpperBound += spawnOdds;
			if(randomFloat >= percentageLowerBound && randomFloat <= percentageUpperBound)
			{
				result = _enemyTypes[i];
				break;
			}
			percentageLowerBound += spawnOdds;
		}

		return result;
	}
}
