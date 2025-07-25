using UnityEngine;


namespace Assets.Scripts.Input
{
	public struct Inputs
	{
		public MovementInputs MovementInputs { get; set; }

		public AbilityInputs AbilityInputs { get; set; }
	}

	public struct MovementInputs
	{
		public bool JumpDown { get; set; }

		public bool JumpHeld { get; set; }

		public Vector2 Move { get; set; }


		public MovementInputs(bool jumpDown, bool jumpHeld, Vector2 move)
		{
			JumpDown = jumpDown;
			JumpHeld = jumpHeld;
			Move = move;
		}
	}

	public struct AbilityInputs
	{
		public bool NetballHeld { get; set; }
	}
}
