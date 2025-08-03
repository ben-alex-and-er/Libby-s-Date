using UnityEngine;


namespace Assets.Scripts.Scenes.Scotland
{
	public class YControlledFog : MonoBehaviour
	{
		[SerializeField]
		private float yStart = 100f;

		[SerializeField]
		private float yEnd = 200f;


		[SerializeField]
		private Vector2 startScale;

		[SerializeField]
		private Vector2 endScale;


		[SerializeField]
		private Transform fog;

		[SerializeField]
		private Transform player;

		private void Awake()
		{
			fog.gameObject.SetActive(true);
		}


		private void Update()
		{
			var percentageThrough = (player.position.y - yStart) / (yEnd - yStart);

			var newSize = percentageThrough switch
			{
				< 0 => startScale,
				> 1 => endScale,
				_ => (endScale - startScale) * percentageThrough + startScale,
			};

			fog.localScale = newSize;
		}
	}
}
