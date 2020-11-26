using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAguila : MonoBehaviour
{

    public Transform target;
    private const float speed = 3;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (target != null)
        {
            target.parent = null;
        }
    }

    void FixedUpdate()
    {
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (transform.position.x <= target.position.x + 0.1)
            spriteRenderer.flipX = false;
            
        else if (transform.position.x >= target.position.x)

            spriteRenderer.flipX = true;
    }
}

