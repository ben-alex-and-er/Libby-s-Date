using System;
using UnityEngine;


namespace Assets.Scripts.Character
{
	public class Character : MonoBehaviour
	{
		public uint currentHealth;

		[Header("Health")]
		[SerializeField]
		private uint maxHealth;


		private void Awake()
		{
			currentHealth = maxHealth;
		}

		public void TakeDamage(uint value)
		{
			currentHealth = Math.Max(currentHealth - value, 0);
		}

		public void Heal(uint value)
		{
			currentHealth = Math.Min(currentHealth + value, maxHealth);
		}

		public bool IsDead()
			=> currentHealth == 0;
	}
}