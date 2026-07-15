using UnityEngine;

public class VFXAnimationEventBridge : MonoBehaviour
{
    public PlayerController player;

    public void DashEndEvent()
    {
        player.BlackDashCDFinished();
    }
}