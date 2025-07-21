using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
	[Header("Speed")]
	[SerializeField]
	private float movementSpeed;

	[SerializeField]
	private float jumpSpeed;


	[Header("Components")]
	[SerializeField]
	private Animator animator;

	[SerializeField]
	private Rigidbody2D rb;

	[SerializeField]
	private Character character;


	private static readonly int running = Animator.StringToHash("Running");
	private static readonly int idle = Animator.StringToHash("Idle");


	private int currentState;


	void Update()
	{
		// Horizontal Movement
		var horizontal = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

		if ((horizontal > 0 && character.facingRight) || (horizontal < 0 && !character.facingRight))
			character.FaceOtherWay();

		// Jump
		var isGrounded = Mathf.Abs(rb.velocity.y) < 0.1f;

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			rb.velocity = new Vector2(rb.velocity.x, 0) + Vector2.up * jumpSpeed;
		}

		// Animations
		var state = GetState();

		if (state != currentState)
		{
			animator.CrossFade(state, 0, 0);
			currentState = state;
		}
	}


	private int GetState()
	{
		var state = rb.velocity.x != 0 ? running : idle;

		return state;
	}
}
