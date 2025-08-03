using UnityEngine;


namespace Assets.Scripts.Scenes.Scotland
{
	public class YControlledFog : MonoBehaviour
	{
		[SerializeField]
		private float yStart = 100f;

		[SerializeField]
		private float yEnd = 200f;


		[SerializeField]
		private Vector2 startScale;

		[SerializeField]
		private Vector2 endScale;


		[SerializeField]
		private Transform fog;

		[SerializeField]
		private Transform player;


		[SerializeField]
		private float lerpSpeed = 5f;

		[SerializeField]
		private float fadeDuration = 2f;

		private bool isFadingOut = false;
		private float fadeTimer = 0f;


		private void Awake()
		{
			fog.gameObject.SetActive(true);
		}


		private void Update()
		{
			if (isFadingOut)
			{
				fadeTimer += Time.deltaTime;
				var lerp = Mathf.Clamp01(fadeTimer / fadeDuration);
				fog.localScale = Vector2.Lerp(endScale, startScale, lerp);

				if (fog.localScale.x == startScale.x && fog.localScale.y == startScale.y)
				{
					fog.gameObject.SetActive(false);
				}

				return;
			}

			var percentageThrough = (player.position.y - yStart) / (yEnd - yStart);

			var targetScale = percentageThrough switch
			{
				< 0 => startScale,
				> 1 => endScale,
				_ => (endScale - startScale) * percentageThrough + startScale,
			};

			fog.localScale = Vector2.Lerp(fog.localScale, targetScale, Time.deltaTime * lerpSpeed);
		}

		public void TriggerFadeOut()
		{
			isFadingOut = true;
			fadeTimer = 0f;
		}
	}
}
