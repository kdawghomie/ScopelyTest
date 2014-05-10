using UnityEngine;
using System.Collections;

public class HowToPlayUI : MonoBehaviour 
{
	public delegate void EventDelegate();
	public event EventDelegate MainMenuPressed;

	void OnMainMenuPressed()
	{
		if(MainMenuPressed != null)
		{
			MainMenuPressed();
		}
	}
}
