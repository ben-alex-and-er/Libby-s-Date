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


		void Start()
		{
			StartCoroutine(LoadSceneSequence());
		}

		IEnumerator LoadSceneSequence()
		{
			yield return StartCoroutine(FadeIn(title, 2f));

			yield return new WaitForSeconds(1f);

			yield return StartCoroutine(FadeIn(subTitle, 2f));

			yield return new WaitForSeconds(1f);

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
