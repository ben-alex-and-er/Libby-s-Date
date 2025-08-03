using UnityEngine;


namespace Assets.Scripts.Trigger
{
	using Scenes.Scotland;


	public class FogFadeTriggerZone : PlayerHitTriggerZone
	{
		[SerializeField]
		private YControlledFog controlledFog;


		protected override void Trigger()
		{
			controlledFog.TriggerFadeOut();
		}

		protected override void ExitTrigger()
		{
			// Nothing
		}
	}
}
