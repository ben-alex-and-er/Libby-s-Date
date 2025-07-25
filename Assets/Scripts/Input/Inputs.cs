using UnityEngine;


namespace Assets.Scripts.Input
{
	public struct Inputs
	{
		public bool JumpDown { get; set; }

		public bool JumpHeld { get; set; }

		public Vector2 Move { get; set; }


		public Inputs(bool jumpDown, bool jumpHeld, Vector2 move)
		{
			JumpDown = jumpDown;
			JumpHeld = jumpHeld;
			Move = move;
		}
	}
}
