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


		public Direction FacingDirection { get; private set; }


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