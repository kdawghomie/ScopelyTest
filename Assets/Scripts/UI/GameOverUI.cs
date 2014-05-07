using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour 
{
	public delegate void EventDelegate();
	public event EventDelegate MainMenuPressed;
	public event EventDelegate RetryPressed;

	private void OnMainMenuPressed()
	{
		if(MainMenuPressed != null)
		{
			MainMenuPressed();
		}
	}

	private void OnRetryPressed()
	{
		if(RetryPressed != null)
		{
			RetryPressed();
		}
	}
}
