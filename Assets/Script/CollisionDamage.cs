﻿using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour {
	public float damage = 1;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player"))
			other.SendMessage ("ApplayDamage", damage);
	}

	void OnTriggeStay2D(Collider2D other)
	{
		if (other.CompareTag ("Player"))
			other.SendMessage ("ApplayDamage", damage);
	}
}
