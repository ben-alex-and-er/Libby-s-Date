using System;
using UnityEngine;


namespace Assets.Scripts.Movement
{
	public class Follower : CharacterMovement
	{
		[Header("Follower")]
		[SerializeField]
		private Transform objectToFollow;

		[SerializeField]
		private float distanceFromObject;


		[Header("Components")]
		[SerializeField]
		private Animator animator;

		[SerializeField]
		private SpriteRenderer spriteRenderer;


		private static readonly int running = Animator.StringToHash("Running");
		private static readonly int idle = Animator.StringToHash("Idle");


		private int currentAnimationState;


		protected override void Move()
		{
			var distance = objectToFollow.position - transform.position;

			if (distance.magnitude < distanceFromObject)
			{
				velocityThisFrame.x = 0;
				return;
			}

			if (distance.x > 0)
			{
				velocityThisFrame.x = Mathf.MoveTowards(velocityThisFrame.x, 1 * movementSpeed, acceleration * Time.fixedDeltaTime);
			}
			else
			{
				velocityThisFrame.x = Mathf.MoveTowards(velocityThisFrame.x, -1 * movementSpeed, acceleration * Time.fixedDeltaTime);
			}
		}

		protected override void Gravity()
		{
			if (isGrounded && velocityThisFrame.y <= 0f)
			{
				velocityThisFrame.y = groundingForce;
			}
			else
			{
				var inAirGravity = gravity;

				velocityThisFrame.y = Mathf.MoveTowards(velocityThisFrame.y, -maxGravitySpeed, inAirGravity * Time.fixedDeltaTime);
			}
		}

		protected override void ChangeAnimations()
		{
			var state = GetAnimationState();

			if (state != currentAnimationState)
			{
				animator.CrossFade(state, 0, 0);
				currentAnimationState = state;
			}

			if (velocityThisFrame.x != 0)
			{
				if (velocityThisFrame.x > 0)
				{
					spriteRenderer.flipX = false;
				}
				else
				{
					spriteRenderer.flipX = true;
				}
			}
		}


		private int GetAnimationState()
		{
			var state = Math.Abs(velocityThisFrame.x) > 0 ? running : idle;

			return state;
		}
	}
}
