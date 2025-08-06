using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Trigger
{
	public class PlayerSpeechImageTriggerZone : PlayerHitTriggerZone
	{
		[SerializeField]
		private Image image;

		[SerializeField]
		private Sprite speechSprite;


		protected override void Trigger()
		{
			image.gameObject.SetActive(true);

			image.sprite = speechSprite;
		}

		protected override void ExitTrigger()
		{
			image.gameObject.SetActive(false);
		}
	}
}
