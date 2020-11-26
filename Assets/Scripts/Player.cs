using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    private const float walkSpeed = 4;
    private const float runSpeed = 6;
    private const float jumpForce = 5;
    private const float doubleJumpForce = 8;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private AnyStateAnimator stateAnimator;
    private AudioSource _audioSource;

    private bool doubleJump = true;
    private bool ground = false;
    //private bool muerte = false;

    public ControlPuntaje Puntaje;
    public ControlVida Vida;

    //public AudioClip AudioJump;
    //public AudioClip AudioAPunch;
    //public AudioClip AudioAGun;
    //public AudioClip AudioASword;
    public AudioClip AudioCoins;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stateAnimator = GetComponent<AnyStateAnimator>();
        _audioSource = GetComponent<AudioSource>();
        stateAnimator.AddAnimation("Idle", "Walk", "Run", "Fall", "Jump", "JumpAttack", "DoubleJump", "SwapWalk", "Attack");

    }

    //void Update()
    //{
    //    //Controles();
    //    HandleAir();
    //    HandleLayers();
    //}
    private void Update()
    {
        rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        if (rigidBody.velocity == Vector2.zero && !stateAnimator.IsAnimationActive("SwapWalk"))
        {
            stateAnimator.TryPlayAnimation("Idle", "Attack", "SwapWalk");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();
        if (Input.GetKeyDown(KeyCode.LeftControl))
            Attack();
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody.velocity = new Vector2(walkSpeed, rigidBody.velocity.y);
            spriteRenderer.flipX = false;
            if(ground)
                stateAnimator.TryPlayAnimation("Walk", "Attack");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidBody.velocity = new Vector2(-walkSpeed, rigidBody.velocity.y);
            spriteRenderer.flipX = true;
            if (ground)
                stateAnimator.TryPlayAnimation("Walk", "Attack");
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            rigidBody.velocity = new Vector2(runSpeed, rigidBody.velocity.y);
            spriteRenderer.flipX = false;
            if(ground)
                stateAnimator.TryPlayAnimation("Run", "Attack");
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift) )
        {
            rigidBody.velocity = new Vector2(-runSpeed, rigidBody.velocity.y);
            spriteRenderer.flipX = true;
            if (ground)
                stateAnimator.TryPlayAnimation("Run", "Attack");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) )
        {
            stateAnimator.ChangeController(0);
            stateAnimator.TryPlayAnimation("SwapWalk");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) )
        {
            stateAnimator.ChangeController(1);
            stateAnimator.TryPlayAnimation("SwapWalk");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) )
        {
            stateAnimator.ChangeController(2);
            stateAnimator.TryPlayAnimation("SwapWalk");
        }
        HandleAir();
        HandleLayers();
    }

    private void Attack()
    {
        if (ground)
        {
            rigidBody.velocity = Vector2.zero;
            stateAnimator.TryPlayAnimation("Attack");
        }
        else
        {
            stateAnimator.TryPlayAnimation("Attack");
        }

    }
    private void Jump()
    {
        if (ground)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            stateAnimator.TryPlayAnimation("Jump");
        }
        else if (doubleJump)
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.AddForce(new Vector2(0, doubleJumpForce), ForceMode2D.Impulse);
            doubleJump = false;
            stateAnimator.TryPlayAnimation("DoubleJump");
        }
    }
    private void HandleAir()
    {
        if (IsFalling())
        {
            stateAnimator.TryPlayAnimation("Fall", "DoubleJump", "Attack");

        }
        else if (!IsFalling() && !ground)
        {
            stateAnimator.TryPlayAnimation("Jump", "DoubleJump", "Attack");
        }
    }

    private bool IsFalling()
    {
        if (rigidBody.velocity.y < 0 && !ground)
        {
            return true;
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!ground)
        {
            stateAnimator.ActivateLayer(1);
        }
        else
        {
            stateAnimator.ActivateLayer(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            doubleJump = true;
            ground = true;
        }
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            transform.parent = collision.transform;
        }
        if (collision.gameObject.CompareTag("Enemy") && !Input.GetKeyDown(KeyCode.LeftControl))
        {
            ground = true;
            doubleJump = true;
            if (Vida.GetVida())
                Vida.RemoveLife(1);
            else if (!Vida.GetVida())
            {
                Vida.GetVida();
                Debug.Log("Estas muerto");//muerte = true;
            }
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            ground = false;
        }
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            transform.parent = null;
        }
        if (collision.gameObject.CompareTag("Enemy") )
        {
            ground = false;
        }
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector3(-6,-3,0);
        if (Vida.GetVida())
            Vida.RemoveLife(1);
        else if (!Vida.GetVida())
        {
            Vida.GetVida();
            Debug.Log("Estas muerto");//muerte = true;
        }
    }

    [System.Obsolete]
    public void LoadNextLevel()
    {

        if (Application.loadedLevel < Application.levelCount - 1)
            Application.LoadLevel(Application.loadedLevel + 1);
    }
    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Nivel"))
        {
            LoadNextLevel();
        }
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
            _audioSource.PlayOneShot(AudioCoins);
            Puntaje.AddPoints(1);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Vida.GetVida())
                Vida.RemoveLife(1);
            else
            {
                Vida.GetVida();
                Debug.Log("Estas muerto");//muerte = true;
            }
        }
    }
}
