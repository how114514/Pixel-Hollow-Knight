using Cinemachine;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
	public GameObject target;

	public Door door;

	public CinemachineConfiner2D confiner;

	public Collider2D bounds;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("PlayerBody"))
		{
			target.SetActive(value: true);
			door?.Close();
			GetComponent<Collider2D>().enabled = false;
			if (confiner != null && bounds != null)
			{
				confiner.m_BoundingShape2D = bounds;
			}
		}
	}
}
