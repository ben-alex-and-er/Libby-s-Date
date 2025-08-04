using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Scripts.Scenes
{
	public class SceneTitleLoader : MonoBehaviour
	{
		[SerializeField]
		private Text title;

		[SerializeField]
		private Text subTitle;

		[SerializeField]
		private string nextScene;


		async void Start()
		{
			StartCoroutine(FadeIn(title, 2));

			await Task.Delay(3000);

			StartCoroutine(FadeIn(subTitle, 2));

			await Task.Delay(3000);

			SceneManager.LoadScene(nextScene);
		}

		static IEnumerator FadeIn(Text text, float fadeDuration)
		{
			var color = text.color;
			color.a = 0f;
			text.color = color;

			var elapsed = 0f;
			while (elapsed < fadeDuration)
			{
				elapsed += Time.deltaTime;
				color.a = Mathf.Clamp01(elapsed / fadeDuration);
				text.color = color;
				yield return null;
			}
		}
	}
}
