﻿using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
	
	private static UIManager _instance;
	
	public static UIManager GetInstance()
	{
		if(_instance == null)
		{
			GameObject gameScreensObject = GameObject.Find("UIManager") as GameObject;
			_instance = gameScreensObject.GetComponent<UIManager>();
		}
		return _instance;
	}
	
	#region objects
	
	[SerializeField] public GameObject GameplayHUD;
	
	#endregion

	#region methods
	
	public GameObject InstantiateForegroundUI(GameObject uiPrefab)
	{
		GameObject result = Instantiate(uiPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		result.transform.parent = (GameObject.Find("ForegroundPanel") as GameObject).transform;
		result.transform.localScale = uiPrefab.transform.localScale;
		result.transform.localPosition = uiPrefab.transform.localPosition;

		return result;
	}
	
	#endregion
}