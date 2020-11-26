using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    public GameObject player;
    private float x, y;
    public Vector2 minCamera, maxCamera;
    private Vector2 velocity;
    private const float smoothTime = 0.15f;

    void FixedUpdate()
    {
        x = Mathf.SmoothDamp(transform.position.x,
            player.transform.position.x + 3, ref velocity.x, smoothTime);
        y = Mathf.SmoothDamp(transform.position.y,
            player.transform.position.y, ref velocity.y, smoothTime);
        transform.position = new Vector3(
            Mathf.Clamp(x, minCamera.x, maxCamera.x),
            Mathf.Clamp(y, minCamera.y, maxCamera.y),
            transform.position.z);
    }
}
