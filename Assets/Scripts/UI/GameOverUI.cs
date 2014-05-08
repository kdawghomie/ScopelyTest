using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour 
{
	// Editor variables
	[SerializeField] private UILabel _scoreTextLabel = null; // the text "Score: "
	[SerializeField] private UILabel _scoreNumberLabel = null; // label for actual score the player got

	// Events
	public delegate void EventDelegate();
	public event EventDelegate MainMenuPressed;
	public event EventDelegate RetryPressed;

	// Private member variables
	private int _score;
	private int _scoreCounter = 0;

	#region Lifecycle
	public void Init(int score)
	{
		_score = score;

		_scoreNumberLabel.gameObject.SetActive(false);
		_scoreTextLabel.gameObject.SetActive(false);

		Invoke("TweenInScoreLabels", 0.5f);
	}
	#endregion

	#region Private helper functions
	private void TweenInScoreLabels()
	{
		_scoreTextLabel.gameObject.SetActive(true);
		_scoreNumberLabel.gameObject.SetActive(true);
		
		iTween.ScaleFrom(_scoreTextLabel.gameObject, iTween.Hash(
			"scale", new Vector3(60f, 60f, 1f),
			"easetype", "easeInOutElastic",
			"time", .3f
		));
		
		iTween.ScaleFrom(_scoreNumberLabel.gameObject, iTween.Hash(
			"scale", new Vector3(60f, 60f, 1f),
			"easetype", "easeInOutElastic",
			"time", .3f
		));

		StartCoroutine("ScoreCount");
	}
	
	private IEnumerator ScoreCount()
	{
		while(_scoreCounter < _score)
		{
			_scoreCounter += 50;
			if(_scoreCounter > _score)
			{
				_scoreCounter = _score;
			}
			_scoreNumberLabel.text = _scoreCounter.ToString();
			yield return new WaitForSeconds(0.01f);
		}
	}
	#endregion

	#region Button messages
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
	#endregion
}
