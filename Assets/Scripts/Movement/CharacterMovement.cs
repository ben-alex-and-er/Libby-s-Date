using UnityEngine;


namespace Assets.Scripts.Movement
{
	using Input;


	public class CharacterMovement : MonoBehaviour
	{
		[Header("Movement")]
		[SerializeField]
		protected float movementSpeed = 14;

		[SerializeField]
		protected float acceleration = 120;

		[SerializeField]
		protected float friction = 60;

		[SerializeField]
		protected float windResistance = 60;


		[Header("Jumping")]
		[SerializeField]
		protected float jumpForce = 36;

		[SerializeField]
		protected float earlyJumpBuffer = 0.2f;


		[Header("Gravity")]
		[SerializeField]
		protected float gravity = 40f;

		[SerializeField]
		protected float maxGravitySpeed = 40f;

		[SerializeField]
		protected float shortJumpGravityModifier = 3f;

		[SerializeField]
		protected float groundingForce = -1.5f;


		[Header("Other")]
		[SerializeField]
		protected float groundedDistanceBuffer = 0.1f;


		[Header("Components")]
		[SerializeField]
		protected Rigidbody2D rb;

		[SerializeField]
		protected CapsuleCollider2D capsuleCollider;

		[SerializeField]
		protected LayerMask playerLayer;


		protected Vector2 velocityThisFrame;
		protected bool isGrounded;
		protected bool shortJump;



		protected void FixedUpdate()
		{
			CheckCollisions();

			Move();

			Jump();

			Gravity();

			ChangeAnimations();

			rb.velocity = velocityThisFrame;
		}


		public virtual void SetInputs(MovementInputs inputs)
		{

		}


		protected virtual void CheckCollisions()
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

		protected virtual void Move()
		{

		}

		protected virtual void Jump()
		{

		}

		protected virtual void Gravity()
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

		protected virtual void ChangeAnimations()
		{

		}


		protected bool CollisionInDirection(Vector2 direction)
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
