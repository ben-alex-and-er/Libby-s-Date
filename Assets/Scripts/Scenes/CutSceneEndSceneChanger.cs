using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.Scenes
{
	public class CutSceneEndSceneChanger : MonoBehaviour
	{
		[SerializeField]
		private string sceneName;


		public void ChangeScene()
		{
			SceneManager.LoadScene(sceneName);
		} 
	}
}
