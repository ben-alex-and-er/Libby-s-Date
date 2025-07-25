using UnityEngine;
using static Assets.Scripts.Character.Character;


namespace Assets.Scripts.Abilities.Netball
{
	public class Netball : MonoBehaviour
	{
		[Header("Movement")]
		[SerializeField]
		private float speed = 10;

		[SerializeField]
		private float maxTravelDistance = 10;

		[SerializeField]
		private float rotationSpeed = 10;


		[Header("Components")]
		[SerializeField]
		private Rigidbody2D rb;

		[SerializeField]
		private Transform returnTarget;

		[SerializeField]
		private NetballAbility ability;


		private float distanceTravelled;
		private Travel travel;


		private void Awake()
		{
			gameObject.SetActive(false);
		}

		private void FixedUpdate()
		{
			if (travel == Travel.RETURNING)
			{
				Rotate();
				Return();
			}
			else if (travel == Travel.TRAVELLING)
			{
				Rotate();

				distanceTravelled += speed * Time.fixedDeltaTime;

				if (distanceTravelled > maxTravelDistance)
				{
					SetTravel(Travel.RETURNING);
				}
			}
		}

		private void Return()
		{
			var distance = returnTarget.position - transform.position;

			if (distance.magnitude < 0.1f)
			{
				Caught();
			}

			var direction = distance.normalized;

			rb.velocity = direction * speed;
		}

		private void Rotate()
		{
			transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
		}

		public void Thrown(Direction direction)
		{
			if (direction == Direction.NONE)
				return;

			SetTravel(Travel.TRAVELLING);

			gameObject.SetActive(true);

			if (direction == Direction.RIGHT)
			{
				rb.velocity = new Vector2(speed, 0);
			}
			else
			{
				rb.velocity = new Vector2(-speed, 0);
			}


			distanceTravelled = 0;
		}

		public void Caught()
		{
			SetTravel(Travel.NONE);

			gameObject.SetActive(false);

			ability.Catch();
		}

		public void HitObject()
		{
			SetTravel(Travel.RETURNING);
		}

		private void SetTravel(Travel travel)
		{
			this.travel = travel;
		}

		private enum Travel
		{
			NONE = 0,
			TRAVELLING = 1,
			RETURNING = 2,
		}
	}
}
