using UnityEngine;


namespace Assets.Scripts.Trigger
{
	using Movement;


	public class ToggleFollowerVisibilityPlayerTriggerZone : PlayerHitTriggerZone
	{
		[SerializeField]
		private Follower follower;

		[SerializeField]
		private bool isFollowerVisible;


		protected override void Trigger()
		{
			follower.transform.position = transform.position;

			follower.ToggleVisibility(isFollowerVisible);
		}


		protected override void ExitTrigger()
		{
			// Do nothing
		}
	}
}
