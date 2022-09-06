
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Photon.Pun;


public class MovementController : MonoBehaviourPunCallbacks
{
    public new Rigidbody2D rigidbody { get; private set; }
    private Vector2 direction = Vector2.down;
    public float speed = 5f;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;


    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;


    PhotonView view;

    
   
    private void Start()
    {
        view = GetComponent<PhotonView>();
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    private void Update()
    {
     
       /*   if(CrossPlatformInputManager.GetButton("InputUp"))
          {
              SetDirection(Vector2.up,spriteRendererUp);
          }else if(CrossPlatformInputManager.GetButton("InputDown"))
          {
              SetDirection(Vector2.down,spriteRendererDown);
          }else if(CrossPlatformInputManager.GetButton("InputLeft"))
          {
              SetDirection(Vector2.left,spriteRendererLeft);
          }else if(CrossPlatformInputManager.GetButton("InputRight"))
          {
              SetDirection(Vector2.right,spriteRendererRight);
          }else
          {
              SetDirection(Vector2.zero,activeSpriteRenderer);
          }*/


       if(view.IsMine)
        {
            if (Input.GetKey(inputUp))
            {
                SetDirection(Vector2.up, spriteRendererUp);
            }
            else if (Input.GetKey(inputDown))
            {
                SetDirection(Vector2.down, spriteRendererDown);
            }
            else if (Input.GetKey(inputLeft))
            {
                SetDirection(Vector2.left, spriteRendererLeft);
            }
            else if (Input.GetKey(inputRight))
            {
                SetDirection(Vector2.right, spriteRendererRight);
            }
            else
            {
                SetDirection(Vector2.zero, activeSpriteRenderer);
            }
        }


      

    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {

            Vector2 position = rigidbody.position;
            Vector2 translation = direction * speed * Time.fixedDeltaTime;

            rigidbody.MovePosition(position + translation);
        }
    }

    void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            DeathSequence();
        }
    }
}
