using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.UI.Healthbar
{
	public class HealthBar : MonoBehaviour
	{
		[SerializeField]
		private List<Heart> hearts;


		public void TakeDamage(uint value)
		{
			for (var i = hearts.Count - 1; i >= 0 && value > 0; i--)
			{
				var heart = hearts[i];

				while (value > 0)
				{
					if (heart.currentState == Heart.HeartState.EMPTY)
						break;

					if (heart.currentState == Heart.HeartState.FULL)
					{
						heart.SetHalf();
						value--;
					}
					else if (heart.currentState == Heart.HeartState.HALF)
					{
						heart.SetEmpty();
						value--;
					}
				}
			}
		}

		public void Heal(uint value)
		{
			for (var i = 0; i <= hearts.Count - 1 && value < 0; i++)
			{
				var heart = hearts[i];

				while (value > 0)
				{
					if (heart.currentState == Heart.HeartState.FULL)
						break;

					if (heart.currentState == Heart.HeartState.EMPTY)
					{
						heart.SetHalf();
						value--;
						break;
					}

					if (heart.currentState == Heart.HeartState.HALF)
					{
						heart.SetFull();
						value--;
						break;
					}
				}
			}
		}
	}
}
