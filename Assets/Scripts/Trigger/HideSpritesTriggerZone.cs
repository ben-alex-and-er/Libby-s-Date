using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Trigger
{
	public class HideSpritesTriggerZone : PlayerHitTriggerZone
	{
		[SerializeField]
		private List<SpriteRenderer> spriteRenderers;


		protected override void Trigger()
		{
			foreach (var obj in spriteRenderers)
			{
				obj.enabled = false;
			}
		}

		protected override void ExitTrigger()
		{
			// Nothing
		}
	}
}
