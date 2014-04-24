using UnityEngine;
using System.Collections;

public class StartScreenUI : MonoBehaviour {

	[SerializeField] private GameObject _playButton;

	public delegate void EventDelegate();
	public event EventDelegate PlayPressed; 

	public void Init()
	{
		TweenInButtons();
	}

	private void TweenInButtons()
	{
		iTween.MoveFrom(_playButton, iTween.Hash(
			"x", -2f,
			"time", 0.8f,
			"easetype", "easeOutBounce"
		));
	}

	private void OnPlayPressed()
	{
		if(PlayPressed != null)
		{
			PlayPressed();
		}
	}
}
