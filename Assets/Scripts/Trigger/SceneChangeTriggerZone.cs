using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.Trigger
{
	public class SceneChangeTriggerZone : PlayerHitTriggerZone
	{
		[SerializeField]
		private string sceneName;


		protected override void Trigger()
		{
			SceneManager.LoadScene(sceneName);
		}

		protected override void ExitTrigger()
		{
			// Nothing
		}
	}
}
