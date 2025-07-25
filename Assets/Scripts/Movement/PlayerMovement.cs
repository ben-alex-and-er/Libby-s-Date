using System;
using UnityEngine;


namespace Assets.Scripts.Movement
{
	using Character;
	using Input;


	/// <summary>
	/// Handles player movement
	/// </summary>
	public class PlayerMovement : MonoBehaviour
	{
		[Header("Movement")]
		[SerializeField]
		private float movementSpeed = 14;

		[SerializeField]
		private float acceleration = 120;

		[SerializeField]
		private float friction = 60;

		[SerializeField]
		private float windResistance = 60;


		[Header("Jumping")]
		[SerializeField]
		private float jumpForce = 36;

		[SerializeField]
		private float earlyJumpBuffer = 0.2f;


		[Header("Gravity")]
		[SerializeField]
		private float gravity = 40f;

		[SerializeField]
		private float maxGravitySpeed = 40f;

		[SerializeField]
		private float shortJumpGravityModifier = 3f;

		[SerializeField]
		private float groundingForce = -1.5f;


		[Header("Other")]
		[SerializeField]
		private float groundedDistanceBuffer = 0.1f;


		[Header("Components")]
		[SerializeField]
		private Animator animator;

		[SerializeField]
		private Rigidbody2D rb;

		[SerializeField]
		private CapsuleCollider2D capsuleCollider;

		[SerializeField]
		private Character character;

		[SerializeField]
		private SpriteRenderer spriteRenderer;

		[SerializeField]
		private PlayerInput playerInput;

		[SerializeField]
		private LayerMask playerLayer;


		private static readonly int running = Animator.StringToHash("Running");
		private static readonly int idle = Animator.StringToHash("Idle");


		private MovementInputs inputs;
		private Vector2 velocityThisFrame;
		private int currentAnimationState;
		private float jumpBufferTimer = 0f;
		private bool isGrounded;
		private bool shortJump;


		private void FixedUpdate()
		{
			CheckCollisions();

			Move();

			Jump();

			Gravity();

			ChangeAnimations();

			rb.velocity = velocityThisFrame;
		}


		public void SetInputs(MovementInputs inputs)
		{
			this.inputs = inputs;

			if (inputs.JumpDown)
			{
				jumpBufferTimer = earlyJumpBuffer;
			}
		}


		private void CheckCollisions()
		{
			var queriesStartInColliders = Physics2D.queriesStartInColliders;
			Physics2D.queriesStartInColliders = false;

			var groundHit = CollisionInDirection(Vector2.down);
			var ceilingHit = CollisionInDirection(Vector2.up);

			if (ceilingHit)
			{
				velocityThisFrame.y = Mathf.Min(0, velocityThisFrame.y);
			}

			if (!isGrounded && groundHit)
			{
				isGrounded = true;
				shortJump = false;
			}
			else if (isGrounded && !groundHit)
			{
				isGrounded = false;
			}

			Physics2D.queriesStartInColliders = queriesStartInColliders;
		}

		private void Move()
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

		private void Jump()
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

		private void Gravity()
		{
			if (isGrounded && velocityThisFrame.y <= 0f)
			{
				velocityThisFrame.y = groundingForce;
			}
			else
			{
				var inAirGravity = gravity;

				if (shortJump && velocityThisFrame.y > 0)
				{
					inAirGravity *= shortJumpGravityModifier;
				}

				velocityThisFrame.y = Mathf.MoveTowards(velocityThisFrame.y, -maxGravitySpeed, inAirGravity * Time.fixedDeltaTime);
			}
		}

		private void ChangeAnimations()
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

		private bool CollisionInDirection(Vector2 direction)
			=> Physics2D.CapsuleCast(
				origin: capsuleCollider.bounds.center,
				capsuleCollider.size,
				capsuleCollider.direction,
				angle: 0,
				direction,
				groundedDistanceBuffer,
				playerLayer);
	}
}