using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.UI.Healthbar
{
	public class Heart : MonoBehaviour
	{
		public HeartState currentState;


		[SerializeField]
		private Image full;

		[SerializeField]
		private Image half;

		[SerializeField]
		private Image empty;


		private void Awake()
		{
			SetFull();
		}

		public void SetState(HeartState state)
		{
			switch (state)
			{
				case HeartState.EMPTY:
					SetEmpty();
					break;
				case HeartState.HALF:
					SetHalf();
					break;
				case HeartState.FULL:
					SetFull();
					break;
				default:
					break;
			}
		}

		public void SetEmpty()
		{
			currentState = HeartState.EMPTY;

			full.gameObject.SetActive(false);
			half.gameObject.SetActive(false);
			empty.gameObject.SetActive(true);
		}

		public void SetHalf()
		{
			currentState = HeartState.HALF;

			full.gameObject.SetActive(false);
			half.gameObject.SetActive(true);
			empty.gameObject.SetActive(false);
		}

		public void SetFull()
		{
			currentState = HeartState.FULL;

			full.gameObject.SetActive(true);
			half.gameObject.SetActive(false);
			empty.gameObject.SetActive(false);
		}


		public enum HeartState
		{
			EMPTY,
			HALF,
			FULL
		}
	}
}
