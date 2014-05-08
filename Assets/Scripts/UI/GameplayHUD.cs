using UnityEngine;
using System.Collections;

public class GameplayHUD : MonoBehaviour 
{
	[SerializeField] private UILabel _healthLabel = null;
	[SerializeField] private UILabel _ammoLabel = null;

	public void Init()
	{
		_healthLabel.text = "100%";
		_ammoLabel.text = "100"; // TODO: create weapon manager class?
	}

	void Update()
	{

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

	public void SetAmmo(int ammo)
	{
		_ammoLabel.text = ammo.ToString();

		iTween.PunchScale(_ammoLabel.gameObject, iTween.Hash(
			"amount", new Vector3(1.5f, 1.5f, 1.5f),
			"easetype", "easeInOutElastic",
			"time", .5f
		));
	}
}
