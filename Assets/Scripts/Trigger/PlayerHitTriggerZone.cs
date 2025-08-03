using UnityEngine;


namespace Assets.Scripts.Trigger
{
	public abstract class PlayerHitTriggerZone : MonoBehaviour
	{
		[SerializeField]
		private string PlayerTag = "Player";


		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag(PlayerTag))
			{
				Debug.Log("Player hit trigger");
				Trigger();
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.CompareTag(PlayerTag))
			{
				Debug.Log("Player left trigger");
				ExitTrigger();
			}
		}

		protected abstract void Trigger();

		protected abstract void ExitTrigger();
	}
}
