using UnityEngine;
using System.Collections;

public class StartScreenUI : MonoBehaviour {

	public delegate void EventDelegate();
	public event EventDelegate PlayPressed; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPlayPressed()
	{
		if(PlayPressed != null)
		{
			PlayPressed();
		}
	}
}
