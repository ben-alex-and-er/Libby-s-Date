using UnityEngine;


public class Character : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight;


	public void FaceOtherWay()
	{
		facingRight = !facingRight;
		transform.Rotate(0f, 180f, 0f);
	}
}
