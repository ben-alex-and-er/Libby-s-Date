using System.Collections;
using System.Collections.Generic;
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


		protected override void Trigger()
		{
			StartCoroutine(FinishLevelRoutine());
		}

		protected override void ExitTrigger()
		{
			// Nothing
		}


		private IEnumerator FinishLevelRoutine()
		{
			playerMovement.SetDance(true);

			foreach (var follower in followers)
			{
				follower.SetDance(true);
			}

			yield return new WaitForSeconds(4f);

			SceneManager.LoadScene(sceneName);
		}
	}
}
