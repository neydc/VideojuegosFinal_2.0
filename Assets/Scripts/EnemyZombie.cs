using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : MonoBehaviour
{
    private float maxSpeed = 2f;
    private float speed = 2f;
    private float limitedSpeed;

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private const int ANIM_ATACAR = 1;
    private const int ANIM_QUIETO = 0;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.AddForce(Vector2.right * speed);
        limitedSpeed = Mathf.Clamp(rb2D.velocity.x, -maxSpeed, maxSpeed);
        rb2D.velocity = new Vector2(limitedSpeed, rb2D.velocity.y);
        
        if(rb2D.velocity.x > -0.01f && rb2D.velocity.x < 0.01)
        {
            speed = -speed;
            rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
        }

        if (speed > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (speed < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetInteger("Estado", ANIM_ATACAR);
            spriteRenderer.flipX = false;
        }else
            animator.SetInteger("Estado", ANIM_QUIETO);

    }

}
