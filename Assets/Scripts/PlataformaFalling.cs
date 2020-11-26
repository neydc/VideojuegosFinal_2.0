using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaFalling : MonoBehaviour
{
    private const float fallDelay = 1f;
    private const float RespDelay = 5f;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private Vector3 start;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        start = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(Fall), fallDelay);
            Invoke(nameof(Respawn), fallDelay + RespDelay);
        }

    }
    void Fall()
    {
        rigidBody.isKinematic = false;
        boxCollider.isTrigger = true;
    }
    void Respawn()
    {
        transform.position = start;
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = true;
        boxCollider.isTrigger = false;
    }
}
