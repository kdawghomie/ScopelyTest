using UnityEngine;
using System.Collections;

public class GameplayHUD : MonoBehaviour {

	[SerializeField] private UILabel _healthLabel = null;
	[SerializeField] private UILabel _ammoLabel = null;

	public void Init()
	{
		SetHealth(100f);
		SetAmmo(100); // TODO: create weapon manager class?
	}

	public void SetHealth(float health)
	{
		_healthLabel.text = ((int)health).ToString() + "%";
	}

	public void SetAmmo(int ammo)
	{
		_ammoLabel.text = ammo.ToString();
	}
}
