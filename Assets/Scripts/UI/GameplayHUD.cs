using UnityEngine;
using System.Collections;

public class GameplayHUD : MonoBehaviour 
{
	// constants
	private const float SCORE_LABEL_INCREMENT_SPEED = 350f;

	// editor variables
	[SerializeField] private UILabel _healthLabel = null;
	[SerializeField] private UILabel _ammoLabel = null;
	[SerializeField] private UILabel _scoreLabel = null;
	[SerializeField] private UILabel _waveCompleteLabel = null;
	[SerializeField] private UITexture _damageIndicator = null;

	// member variables
	private int _score = 0;
	private int _currentLabelScore = 0;

	#region Properties
	public int Score
	{
		get{ return _score; }
	}
	#endregion

	#region Lifecycle
	public void Init()
	{
		_waveCompleteLabel.alpha = 0f;
		_damageIndicator.alpha = 0f;
		_healthLabel.text = "100%";
		_ammoLabel.text = "100"; // TODO: create weapon manager class?
	}

	private void Update()
	{
		UpdateScoreLabel(); 
	}
	#endregion

	#region Private
	private void UpdateScoreLabel()
	{
		if(_currentLabelScore < _score)
		{
			_currentLabelScore += (int)(SCORE_LABEL_INCREMENT_SPEED * Time.deltaTime);
			if(_currentLabelScore > _score)
			{
				_currentLabelScore = _score;
			}
			_scoreLabel.text = _currentLabelScore.ToString();
		}
	}

	private void FadeOutWaveCompleteLabel()
	{
		TweenAlpha.Begin(_waveCompleteLabel.gameObject, 3.6f, 0f);
	}

	private void FadeOutDamageIndicator()
	{
		TweenAlpha.Begin(_damageIndicator.gameObject, 1f, 0f);
	}
	#endregion

	#region Exposed
	public void AddScore(int addScore)
	{
		_score += addScore;

		iTween.PunchScale(_scoreLabel.gameObject, iTween.Hash(
			"amount", new Vector3(3.2f, 3.2f, 3.2f),
			"easetype", "easeInOutElastic",
			"time", .7f
		));
	}

	public void SetHealth(float health)
	{
		_healthLabel.text = ((int)health).ToString() + "%";

		iTween.PunchScale(_healthLabel.gameObject, iTween.Hash(
			"amount", new Vector3(1.5f, 1.5f, 1.5f),
			"easetype", "easeInOutElastic",
			"time", .5f
		));
	}

	public void SetAmmo(int ammo, bool usePunchScale)
	{
		_ammoLabel.text = ammo.ToString();

		if(usePunchScale)
		{
			iTween.PunchScale(_ammoLabel.gameObject, iTween.Hash(
				"amount", new Vector3(1.5f, 1.5f, 1.5f),
				"easetype", "easeInOutElastic",
				"time", .5f
			));
		}
	}

	public void DisplayWaveCompleteLabel()
	{
		_waveCompleteLabel.alpha = 1f;
		float scaleFromTime = 0.2f;

		iTween.ScaleFrom(_waveCompleteLabel.gameObject, iTween.Hash(
			"scale", new Vector3(60f, 60f, 1f),
			"easetype", "easeInOutExpo",
			"time", scaleFromTime
		));

		Invoke("FadeOutWaveCompleteLabel", scaleFromTime);
	}

	public void DisplayDamageIndicator()
	{
		TweenAlpha.Begin(_damageIndicator.gameObject, 0.15f, 1f);
		Invoke("FadeOutDamageIndicator", 0.7f);
	}
	#endregion
}
