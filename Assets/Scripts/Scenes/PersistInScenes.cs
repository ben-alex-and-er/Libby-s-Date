using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.Scenes
{
	public class PersistInScenes : MonoBehaviour
	{
		[SerializeField]
		private List<string> scenes;


		void Awake()
		{
			if (!scenes.Contains(SceneManager.GetActiveScene().name))
			{
				Destroy(gameObject);
				return;
			}

			DontDestroyOnLoad(gameObject);

			SceneManager.sceneLoaded += RemoveOnInvalidScene;
		}

		private void RemoveOnInvalidScene(Scene scene, LoadSceneMode mode)
		{
			if (scenes.Contains(scene.name))
				return;

			SceneManager.sceneLoaded -= RemoveOnInvalidScene;

			Destroy(gameObject);
		}
	}
}
