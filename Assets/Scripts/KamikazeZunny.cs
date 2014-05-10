using UnityEngine;
using System.Collections;

public class KamikazeZunny : Enemy 
{
	[SerializeField] protected GameObject _kamikazeExplosionPrefab = null;

	override protected void AttackPlayer()
	{
		if(!_dead)
		{
			_player.Attack(ATTACK_DAMAGE);
			GameObject.Instantiate(_kamikazeExplosionPrefab, this.transform.position, Quaternion.identity);
			TakeDamage(HEALTH); // suicide!
			AnimateExplosion(_player.transform.position, 2000f, 50f); // fake some explosion data so enemy detonates in direction away from player
		}
	}	
}
