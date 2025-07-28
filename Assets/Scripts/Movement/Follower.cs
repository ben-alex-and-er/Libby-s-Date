using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Movement
{
	public class Follower : MonoBehaviour
	{
		[Header("Follower")]
		[SerializeField]
		private Transform objectToFollow;

		[SerializeField]
		private Vector3 offset;

		[SerializeField]
		private float delay = 0.3f;

		[SerializeField]
		private float xLerp = 10f;

		[SerializeField]
		private float yLerp = 100f;


		[Header("Components")]
		[SerializeField]
		private Animator animator;

		[SerializeField]
		private SpriteRenderer spriteRenderer;


		private static readonly int running = Animator.StringToHash("Running");
		private static readonly int idle = Animator.StringToHash("Idle");

		private readonly Queue<(float time, Vector3 position)> positionQueue = new();


		private int currentAnimationState;


		void Update()
		{
			// Record player position at intervals
			positionQueue.Enqueue((Time.time, objectToFollow.position));

			if (Time.time > positionQueue.Peek().time + delay)
			{
				var (_, position) = positionQueue.Dequeue();

				MoveTowards(position);
			}
		}

		private void MoveTowards(Vector3 position)
		{
			var adjustedPosition = position + offset;

			var state = GetAnimationState(adjustedPosition);

			if (state != currentAnimationState)
			{
				animator.CrossFade(state, 0, 0);
				currentAnimationState = state;
			}

			if (!IsVeryClose(adjustedPosition.x, transform.position.x))
			{
				spriteRenderer.flipX = adjustedPosition.x <= transform.position.x;
			}

			var x = Mathf.Lerp(transform.position.x, adjustedPosition.x, Time.deltaTime * xLerp);
			var y = Mathf.Lerp(transform.position.y, adjustedPosition.y, Time.deltaTime * yLerp);

			transform.position = new Vector3(x, y);
		}

		private int GetAnimationState(Vector3 target)
		{
			var state = IsVeryClose(target.x, transform.position.x) ? idle : running;

			return state;
		}

		private bool IsVeryClose(float float1, float float2)
			=> Mathf.Abs(float1 - float2) < 0.01f;
	}
}
