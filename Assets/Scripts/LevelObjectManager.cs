using UnityEngine;
using System.Collections;

public class LevelObjectManager : MonoBehaviour {

	private static LevelObjectManager _instance;

	public static LevelObjectManager GetInstance()
	{
		if(_instance == null)
		{
			GameObject gameScreensObject = GameObject.Find("LevelObjectManager") as GameObject;
			_instance = gameScreensObject.GetComponent<LevelObjectManager>();
		}
		return _instance;
	}

	#region objects

	[SerializeField] private GameObject _level1;

	#endregion

	#region methods

	public GameObject InstantiateLevel(string levelName)
	{
		GameObject result = null;

		if(levelName == "level1")
		{
			result = Instantiate(_level1, Vector3.zero, Quaternion.identity) as GameObject;
		}
		else
		{
			Debug.LogError("Level not found.");
		}

		return result;
	}

	#endregion
}
