using UnityEngine;
using UnityInput = UnityEngine.Input;


namespace Assets.Scripts.Input
{
	using Abilities;
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
		public Inputs inputs = new();

		[Header("Movement")]
		[SerializeField]
		private string jumpInput = "Jump";

		[SerializeField]
		private string horizontalInput = "Horizontal";

		[SerializeField]
		private string verticalInput = "Vertical";


		[Header("Abilities")]
		[SerializeField]
		private string netballInput = "Netball";


		[Header("Components")]
		[SerializeField]
		private PlayerMovement playerMovement;

		[SerializeField]
		private Abilities abilities;


		private void Awake()
		{
			Update();
		}


		private void Update()
		{
			var movementInputs = new MovementInputs()
			{
				JumpDown = UnityInput.GetButtonDown(jumpInput),
				JumpHeld = UnityInput.GetButton(jumpInput),
				Move = new Vector2(UnityInput.GetAxisRaw(horizontalInput), UnityInput.GetAxisRaw(verticalInput)),
			};

			playerMovement.SetInputs(movementInputs);

			var abilityInputs = new AbilityInputs
			{
				NetballHeld = UnityInput.GetButton(netballInput)
			};

			abilities.SetInputs(abilityInputs);
		}
	}
}
