using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Trigger
{
	public class DisableGameObjectsTriggerZone : PlayerHitTriggerZone
	{
		[SerializeField]
		private List<GameObject> gameObjects;


		protected override void Trigger()
		{
			foreach(var obj in gameObjects)
			{
				obj.SetActive(false);
			}
		}

		protected override void ExitTrigger()
		{
			// Nothing
		}
	}
}
