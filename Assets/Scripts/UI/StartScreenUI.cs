using UnityEngine;
using System.Collections.Generic;

public class StartScreenUI : MonoBehaviour 
{
	[SerializeField] private UILabel _titleLabel;
	[SerializeField] private List<GameObject> _menuButtons;

	public delegate void EventDelegate();
	public event EventDelegate PlayPressed; 

	public void Init()
	{
		_titleLabel.gameObject.SetActive(false);
		foreach(GameObject menuButton in _menuButtons)
		{
			menuButton.SetActive(false);
		}

		Invoke("TweenInTitleLabel", 0.5f);
		Invoke("TweenInButtons", 1.5f);
	}

	private void TweenInTitleLabel()
	{
		_titleLabel.gameObject.SetActive(true);

		iTween.ScaleFrom(_titleLabel.gameObject, iTween.Hash(
			"scale", new Vector3(60f, 60f, 1f),
			"easetype", "easeOutElastic",
			"time", .28f
		));
	}

	private void TweenInButtons()
	{
		for(int i = 0; i < _menuButtons.Count; i++)
		{
			GameObject menuButton = _menuButtons[i];

			menuButton.SetActive(true);
			float tweenDelay = i*0.2f;

			iTween.MoveFrom(menuButton, iTween.Hash(
				"x", -3.5f,
				"time", 0.8f,
				"easetype", "easeOutBounce",
				"delay", tweenDelay
			));
		}
	}

	private void OnPlayPressed()
	{
		if(PlayPressed != null)
		{
			PlayPressed();
		}
	}
}
