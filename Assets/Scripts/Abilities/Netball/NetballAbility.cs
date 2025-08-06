using UnityEngine;


namespace Assets.Scripts.Abilities.Netball
{
	using Character;


	public class NetballAbility : MonoBehaviour
	{
		public bool foundNetball;


		[SerializeField]
		private Netball netball;


		private bool hasNetball;


		private void Awake()
		{
			hasNetball = true;
		}

		public void TryThrowNetball(CharacterDirection.Direction direction)
		{
			if (!foundNetball)
				return;

			if (direction == CharacterDirection.Direction.NONE)
				return;

			if (hasNetball)
				ThrowNetball(direction);
		}

		public void Catch()
		{
			hasNetball = true;
		}

		private void ThrowNetball(CharacterDirection.Direction direction)
		{
			netball.Thrown(direction);
			hasNetball = false;
		}
	}
}
