using UnityEngine;


namespace Assets.Scripts.Trigger
{
	public class PlayerSpeechTriggerZone : PlayerHitTriggerZone
	{
		[SerializeField]
		private SpriteRenderer spriteRenderer;

		[SerializeField]
		private Sprite speechSprite;

		[SerializeField]
		private Vector2 customPosition;



		protected override void Trigger()
		{
			spriteRenderer.transform.localPosition = customPosition;

			spriteRenderer.gameObject.SetActive(true);

			spriteRenderer.sprite = speechSprite;
		}

		protected override void ExitTrigger()
		{
			spriteRenderer.gameObject.SetActive(false);
		}
	}
}
