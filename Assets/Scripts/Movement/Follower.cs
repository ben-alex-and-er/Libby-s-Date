using System.Collections.Generic;
using System.Linq;
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

			if (adjustedPosition.x != transform.position.x)
			{
				spriteRenderer.flipX = adjustedPosition.x <= transform.position.x;
			}

			transform.position = adjustedPosition;
		}

		private int GetAnimationState(Vector3 target)
		{
			var state = target.x != transform.position.x ? running : idle;

			return state;
		}
	}
}
