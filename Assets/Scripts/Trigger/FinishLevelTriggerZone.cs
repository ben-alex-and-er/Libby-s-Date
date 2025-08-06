using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.Trigger
{
	using Movement;


	public class FinishLevelTriggerZone : PlayerHitTriggerZone
	{
		[SerializeField]
		private string sceneName;

		[SerializeField]
		private PlayerMovement playerMovement;

		[SerializeField]
		private List<Follower> followers;


		protected override async void Trigger()
		{
			playerMovement.SetDance(true);

			foreach (var follower in followers)
			{
				follower.SetDance(true);
			}

			await Task.Delay(4000);

			SceneManager.LoadScene(sceneName);
		}

		protected override void ExitTrigger()
		{
			// Nothing
		}
	}
}
