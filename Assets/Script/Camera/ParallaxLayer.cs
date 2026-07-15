using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public Transform cam;
    public float followCameraX;

    private Vector3 startPos;
    private Vector3 camStartPos;

    private void Start()
    {
        startPos = transform.position;
        camStartPos = cam.position;
    }

    private void LateUpdate()
    {
        float xOffset = (cam.position.x - camStartPos.x) * followCameraX;

        transform.position = new Vector3(
            startPos.x + xOffset,
            startPos.y,
            startPos.z
        );
    }
}