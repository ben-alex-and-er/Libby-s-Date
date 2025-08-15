using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.UI
{
	public class FadeToBlack : MonoBehaviour
	{
		[SerializeField]
		private Image image;

		[SerializeField]
		private float fadeDuration = 1f;


		public void StartFade()
		{
			StartCoroutine(FadeAndChangeScene());
		}


		private IEnumerator FadeAndChangeScene()
		{
			image.gameObject.SetActive(true);

			var startColor = Color.black;

			startColor.a = 0f;

			image.color = startColor;

			var elapsed = 0f;
			var color = startColor;

			while (elapsed < fadeDuration)
			{
				elapsed += Time.deltaTime;

				color.a = Mathf.Clamp01(elapsed / fadeDuration);
				image.color = color;

				yield return null;
			}
		}
	}
}
