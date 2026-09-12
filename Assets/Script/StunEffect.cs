using UnityEngine;

public class StunEffect : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("PlayerBody"))
		{
			Player componentInParent = other.GetComponentInParent<Player>();
			if (componentInParent != null)
			{
				componentInParent.stunnedActive = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("PlayerBody"))
		{
			Player componentInParent = other.GetComponentInParent<Player>();
			if (componentInParent != null)
			{
				componentInParent.stunnedActive = false;
			}
		}
	}
}
