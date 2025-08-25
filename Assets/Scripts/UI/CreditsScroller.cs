using UnityEngine;


public class CreditsScroller : MonoBehaviour
{
	public float scrollSpeed = 50f;


	void Update()
	{
		transform.Translate(scrollSpeed * Time.deltaTime * Vector3.up);
	}
}