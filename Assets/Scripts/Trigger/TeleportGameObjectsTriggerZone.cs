using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Trigger
{
	public class TeleportGameObjectsTriggerZone : PlayerHitTriggerZone
	{
		[SerializeField]
		private List<GameObject> gameObjects;


		protected override void Trigger()
		{
			foreach (var obj in gameObjects)
			{
				obj.transform.position = transform.position;
			}
		}

		protected override void ExitTrigger()
		{
			// Nothing
		}
	}
}
