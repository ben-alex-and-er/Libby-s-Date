using System;
using UnityEngine;


namespace Assets.Scripts.Character
{
	using UI.Healthbar;


	public class Character : MonoBehaviour
	{
		public uint currentHealth;

		[Header("Health")]
		[SerializeField]
		private uint maxHealth;

		[SerializeField]
		private HealthBar healthBar;


		private void Awake()
		{
			currentHealth = maxHealth;
		}

		public void TakeDamage(uint value)
		{
			var damage = Math.Min(value, currentHealth);

			currentHealth -= damage;

			healthBar.TakeDamage(damage);
		}

		public void Heal(uint value)
		{
			var heal = maxHealth - currentHealth;

			value = Math.Min(value, heal);

			currentHealth += value;
		}

		public bool IsDead()
			=> currentHealth == 0;
	}
}