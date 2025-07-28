using UnityEngine;
using UnityInput = UnityEngine.Input;


namespace Assets.Scripts.Input
{
	using Movement;


	/// <summary>
	/// Handles player input
	/// </summary>
	public class PlayerInput : MonoBehaviour
	{
		/// <summary>
		/// Movement and jump inputs per update
		/// </summary>
		[HideInInspector]
		public Inputs inputs = new(false, false, Vector2.zero);


		[SerializeField]
		private string jumpInput = "Jump";

		[SerializeField]
		private string horizontalInput = "Horizontal";

		[SerializeField]
		private string verticalInput = "Vertical";

		[SerializeField]
		private PlayerMovement playerMovement;


		private void Awake()
		{
			playerMovement.SetInputs(inputs);
		}


		private void Update()
		{
			var move = new Vector2(UnityInput.GetAxisRaw(horizontalInput), UnityInput.GetAxisRaw(verticalInput));

			inputs = new Inputs(UnityInput.GetButtonDown(jumpInput), UnityInput.GetButton(jumpInput), move);

			playerMovement.SetInputs(inputs);
		}
	}
}
