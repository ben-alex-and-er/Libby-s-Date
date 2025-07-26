using Assets.Scripts.UI.Healthbar;
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

		[SerializeField]
		private HealthBar healthBar;


		public Direction FacingDirection { get; private set; }


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


		public void FaceNone()
		{
			FacingDirection = Direction.NONE;
		}

		public void FaceRight()
		{
			FacingDirection = Direction.RIGHT;
		}

		public void FaceLeft()
		{
			FacingDirection = Direction.LEFT;
		}


		public enum Direction
		{
			NONE,
			LEFT,
			RIGHT
		}
	}
}