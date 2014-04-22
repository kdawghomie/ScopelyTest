using UnityEngine;
using System.Collections;

public class GameScreens : MonoBehaviour {

	private static GameScreens _instance;

	public static GameScreens GetInstance()
	{
		if(_instance == null)
		{
			GameObject gameScreensObject = GameObject.Find("GameScreens") as GameObject;
			_instance = gameScreensObject.GetComponent<GameScreens>();
		}
		return _instance;
	}

	#region objects

	[SerializeField] private GameObject _level1;

	public GameObject Level1
	{
		get{ return _level1; }
	}

	#endregion

	#region methods

	public GameObject InstantiateGameObject(GameObject gameObject)
	{
		GameObject result = Instantiate(gameObject, Vector3.zero, Quaternion.identity) as GameObject;
		return result;
	}

	#endregion
}
