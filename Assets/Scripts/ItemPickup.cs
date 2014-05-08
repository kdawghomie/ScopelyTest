using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour 
{
	protected void Awake()
	{
		AnimatePickup();
	}

	protected void AnimatePickup()
	{
		iTween.RotateBy(this.gameObject, iTween.Hash(
			"amount", new Vector3(0f, 1f, 0f),
			"time", 3f,
			"looptype", "loop"
		));
		
		iTween.MoveAdd(this.gameObject, iTween.Hash(
			"amount", new Vector3(0f, -5f, 0f),
			"time", 1.5f,
			"looptype", "pingpong"
		));
	}
}
