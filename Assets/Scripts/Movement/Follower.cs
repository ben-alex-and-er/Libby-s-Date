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

		[SerializeField]
		private float minSpeedPercentage = 1f;


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

		public void ToggleVisibility(bool enabled)
		{
			spriteRenderer.enabled = enabled;

			if (!enabled)
				return;

			// Reset queue and physics state
			positionQueue.Clear();

			Vector3 now = objectToFollow.position;
			positionQueue.Enqueue((Time.time - delay, now));
			newPos = now;

			rb.linearVelocity = Vector2.zero;
			velocityThisFrame = Vector2.zero;
			shortJump = false;
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
			var toTarget = newPos.x - transform.position.x;
			var absDelta = Mathf.Abs(toTarget);

			if (absDelta <= followRange)
			{
				velocityThisFrame.x = 0f;
				return;
			}

			var direction = Mathf.Sign(toTarget);

			var distanceFactor = Mathf.InverseLerp(followRange, followRange * 2f, absDelta);
			distanceFactor = Mathf.Clamp(distanceFactor, minSpeedPercentage, 1f);

			velocityThisFrame.x = Mathf.MoveTowards(
				velocityThisFrame.x,
				direction * movementSpeed * distanceFactor,
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
			var toTarget = objectToFollow.position.x - transform.position.x;

			var horizontal = Mathf.Abs(toTarget) <= followRange
				? Vector2.zero
				: new Vector2(Mathf.Sign(toTarget), 0f);

			var targetIsAbove = objectToFollow.position.y > transform.position.y + 0.5f;
			var obstacleAhead = horizontal != Vector2.zero && CollisionInDirection(horizontal);

			var jump = targetIsAbove || obstacleAhead;

			fakeInputs = new MovementInputs(jump, jump, horizontal);
		}

		private int GetAnimationState(float movementSpeed)
		{
			if (isDancing)
				return dancing;

			return movementSpeed > 0.01f ? running : idle;
		}
	}
}
