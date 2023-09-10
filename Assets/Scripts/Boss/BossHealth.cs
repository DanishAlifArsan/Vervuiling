using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 300;
	[SerializeField] private GameObject finishLine;
	
	public GameObject deathEffect;
	public bool isInvulnerable = false;
	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;
			

		health -= damage;
		GetComponent<Animator>().SetBool("Hurt", true);

		// if (health <= 100)
		// {
		// 	GetComponent<Animator>().SetBool("IsEnraged", true);
		// }

		if (health <= 0)
		{
			GetComponent<Animator>().SetBool("Dead", true);
		}
	}

	void Die()
	{
		Destroy(gameObject);
		finishLine.SetActive(true);
	}
}
