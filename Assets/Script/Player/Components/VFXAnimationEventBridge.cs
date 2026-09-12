using UnityEngine;

public class VFXAnimationEventBridge : MonoBehaviour
{
	public Player player;

	public void OnChargeComplete()
	{
		player.canBlackDash = true;
		player.audio.PlayBlackDashReady();
	}
}
