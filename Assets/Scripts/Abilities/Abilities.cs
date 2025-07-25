using UnityEngine;


namespace Assets.Scripts.Abilities
{
	using Character;
	using Input;
	using Netball;


	public class Abilities : MonoBehaviour
	{
		private AbilityInputs inputs;


		[SerializeField]
		private Character character;


		[SerializeField]
		private NetballAbility netballAbility;


		public void SetInputs(AbilityInputs inputs)
		{
			this.inputs = inputs;
		}

		public void FixedUpdate()
		{
			if (inputs.NetballHeld)
				netballAbility.TryThrowNetball(character.FacingDirection);
		}
	}
}
