using System.Collections;
using Unity.VisualScripting;
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

			image.color = Color.black;
			image.color.WithAlpha(0);

			var elapsed = 0f;
			var color = image.color;

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
