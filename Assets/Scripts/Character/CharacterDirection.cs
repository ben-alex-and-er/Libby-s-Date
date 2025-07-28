using UnityEngine;


namespace Assets.Scripts.Character
{
	public class CharacterDirection : MonoBehaviour
	{
		public Direction FacingDirection { get; private set; }

		public void FaceNone()
		{
			FacingDirection = Direction.NONE;
		}

		public void FaceRight()
		{
			FacingDirection = Direction.RIGHT;
		}

		public void FaceLeft()
		{
			FacingDirection = Direction.LEFT;
		}


		public enum Direction
		{
			NONE,
			LEFT,
			RIGHT
		}
	}
}
