using System;
using UnityEngine;


namespace Assets.Scripts.Movement
{
	using Character;
	using Input;


	/// <summary>
	/// Handles player movement
	/// </summary>
	public class PlayerMovement : CharacterMovement
	{
		[Header("Components")]
		[SerializeField]
		private Animator animator;

		[SerializeField]
		private Character character;

		[SerializeField]
		private SpriteRenderer spriteRenderer;


		private static readonly int running = Animator.StringToHash("Running");
		private static readonly int idle = Animator.StringToHash("Idle");


		private MovementInputs inputs;
		private int currentAnimationState;
		private float jumpBufferTimer = 0f;


		public override void SetInputs(MovementInputs inputs)
		{
			this.inputs = inputs;

			if (inputs.JumpDown)
			{
				jumpBufferTimer = earlyJumpBuffer;
			}
		}

		protected override void Move()
		{
			if (inputs.Move.x == 0)
			{
				var deceleration = isGrounded
					? friction
					: windResistance;

				velocityThisFrame.x = Mathf.MoveTowards(velocityThisFrame.x, 0, deceleration * Time.fixedDeltaTime);
			}
			else
			{
				velocityThisFrame.x = Mathf.MoveTowards(velocityThisFrame.x, inputs.Move.x * movementSpeed, acceleration * Time.fixedDeltaTime);
			}
		}

		protected override void Jump()
		{
			if (jumpBufferTimer > 0f)
			{
				jumpBufferTimer -= Time.fixedDeltaTime;
			}

			if (!shortJump && !isGrounded && !inputs.JumpHeld && rb.velocity.y > 0)
			{
				shortJump = true;
			}

			if (isGrounded && jumpBufferTimer > 0f)
			{
				shortJump = false;
				jumpBufferTimer = 0f;

				velocityThisFrame.y = jumpForce;
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
					character.FaceRight();
					spriteRenderer.flipX = false;
				}
				else
				{
					character.FaceLeft();
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