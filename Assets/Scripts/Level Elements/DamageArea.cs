﻿using System;
using UnityEngine;

// Jonatan
[RequireComponent(typeof(Collider2D))]
public class DamageArea : MonoBehaviour
{
	private enum DamageMode
	{
		Single,
		Continuous,
	}

	[SerializeField] private DamageMode mode = DamageMode.Single;
	[SerializeField] private float damage = 10;
	[SerializeField] private float damageCooldown = 0;

	private float cooldownTimer;

	private void OnTriggerEnter2D(Collider2D other) => OnCollisionEvent(other, true);
	private void OnCollisionEnter2D(Collision2D collision) => OnCollisionEvent(collision.collider, true);
	private void OnTriggerStay2D(Collider2D other) => OnCollisionEvent(other, false);
	private void OnCollisionStay2D(Collision2D collision) => OnCollisionEvent(collision.collider, false);

	private void Update()
	{
		cooldownTimer = Mathf.Max(0, cooldownTimer - Time.deltaTime);
	}

	private void OnCollisionEvent(Collider2D other, bool isEnter)
	{
		if (isEnter != (mode == DamageMode.Single)) return;
		if (cooldownTimer != 0) return;

		if (other.CompareTag("Player"))
		{
			cooldownTimer = damageCooldown;
			Player player = other.GetComponent<Player>();
			player.TakeDamage(isEnter ? damage : damage * Time.deltaTime);
		}
	}
}