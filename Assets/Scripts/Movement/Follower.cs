using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Movement
{
	using Input;


	public class Follower : CharacterMovement
	{
		[Header("Follower")]
		[SerializeField]
		private Transform objectToFollow;

		[SerializeField]
		private float delay = 0.3f;

		[SerializeField]
		private float xLerp = 10f;

		[SerializeField]
		private float yLerp = 100f;

		[SerializeField]
		private float followRange = 1.5f;


		[Header("Components")]
		[SerializeField]
		private Animator animator;

		[SerializeField]
		private SpriteRenderer spriteRenderer;


		private static readonly int running = Animator.StringToHash("Running");
		private static readonly int idle = Animator.StringToHash("Idle");
		private static readonly int dancing = Animator.StringToHash("Dancing");

		private readonly Queue<(float time, Vector3 position)> positionQueue = new();


		private int currentAnimationState;
		private MovementInputs fakeInputs = new();
		private Vector3 newPos;


		public void SetDance(bool isDancing)
		{
			this.isDancing = isDancing;
		}


		void FixedUpdate()
		{
			// Record player position at intervals
			positionQueue.Enqueue((Time.time, objectToFollow.position));

			if (Time.time > positionQueue.Peek().time + delay)
			{
				var (_, position) = positionQueue.Dequeue();
				newPos = position;
			}

			CreateFakeInput();

			base.FixedUpdate();
		}


		protected override void Move()
		{
			var toTarget = newPos - transform.position;

			if (toTarget.magnitude < followRange)
			{
				velocityThisFrame.x = 0f;
				return;
			}

			var direction = Mathf.Sign(newPos.x - transform.position.x);

			velocityThisFrame.x = Mathf.MoveTowards(
				velocityThisFrame.x,
				direction * movementSpeed,
				acceleration * Time.fixedDeltaTime);
		}

		protected override void Jump()
		{
			if (!shortJump && !isGrounded && !fakeInputs.JumpHeld && rb.linearVelocity.y > 0)
			{
				shortJump = true;
			}

			if (isGrounded && (fakeInputs.JumpDown || fakeInputs.JumpHeld) && fakeInputs.Move != Vector2.zero)
			{
				shortJump = false;
				velocityThisFrame.y = jumpForce;
			}
		}

		protected override void ChangeAnimations()
		{
			var direction = Mathf.Sign(newPos.x - transform.position.x);

			var movementSpeedAbs = Mathf.Abs(velocityThisFrame.x);

			var state = GetAnimationState(movementSpeedAbs);

			if (state != currentAnimationState)
			{
				animator.CrossFade(state, 0, 0);
				currentAnimationState = state;
			}

			if (movementSpeedAbs > 0.05f)
			{
				spriteRenderer.flipX = direction < 0;
			}
		}


		private void CreateFakeInput()
		{
			var toTarget = newPos - transform.position;

			var horizontal = Mathf.Abs(toTarget.x) > followRange
				? new Vector2(Mathf.Sign(toTarget.x), 0)
				: Vector2.zero;

			var targetIsAbove = newPos.y > transform.position.y + 0.05f;

			var shouldJump = targetIsAbove || (horizontal != Vector2.zero && CollisionInDirection(horizontal));

			fakeInputs = new MovementInputs(shouldJump, shouldJump, horizontal);
		}

		private int GetAnimationState(float movementSpeed)
		{
			if (isDancing)
				return dancing;

			return movementSpeed > 0.01f ? running : idle;
		}
	}
}
