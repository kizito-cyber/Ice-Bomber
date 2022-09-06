using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
	public new Rigidbody2D rigidbody { get; private set; }
	private Vector2 direction = Vector2.left;
	public float speed = 5f;

   
	public AnimatedSpriteRenderer spriteRendererUp;
	public AnimatedSpriteRenderer spriteRendererDown;
	public AnimatedSpriteRenderer spriteRendererLeft;
	public AnimatedSpriteRenderer spriteRendererRight;
	public AnimatedSpriteRenderer spriteRendererDeath;
	private AnimatedSpriteRenderer activeSpriteRenderer;


	public Transform groundDetection;
	public float distance;
	public LayerMask groundLayer;

	public bool movingLeft = true;

	public int enemyLives;
	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		activeSpriteRenderer = spriteRendererLeft;
	}
	private void Start()
	{
		
		EnemyAnimation();
	}

	private void Update()
	{
		if (Physics2D.OverlapCircle(groundDetection.position, 0.3f, groundLayer))
		{
			ChooseDirection();
		}
		

		/*if(Physics2D.OverlapCircle(groundDetection.position,0.3f,groundLayer))
		{
		   // SetDirection(Vector2.right, spriteRendererRight);

			if(groundDetection.position.x<transform.position.x)
			{
				SetDirection(Vector2.right, spriteRendererRight);
				groundDetection.localPosition = new Vector2(0.5f, 0);
			   
			}
			else
			{
				SetDirection(Vector2.left, spriteRendererLeft);
				groundDetection.localPosition = new Vector2(-0.5f, 0);
			}
		}
	  */


	}

	private void FixedUpdate()
	{
		Vector2 position = rigidbody.position;
		Vector2 translation = direction * speed * Time.fixedDeltaTime;

		rigidbody.MovePosition(position + translation);
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
		
		if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
		{
			if(enemyLives<1)
			{
				DeathSequence();
				
			}
			else
			{
				enemyLives--;
			}
			
		}
	}

	void DeathSequence()
	{
		enabled = false;
		//GetComponent<BombController>().enabled = false;

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
		//FindObjectOfType<GameManager>().CheckWinState();
		EnemyManger.instance.enemyCount--;
	}
	public void ChooseDirection()
	{
		Vector2 up = new Vector2(0, 0.1f);
		Vector2 down = new Vector2(0, -0.1f);
		Vector2 right = new Vector2(0.1f, 0);
		Vector2 left = new Vector2(-0.1f, 0);

		List<int> Directions = new List<int>();
		if (Physics2D.OverlapCircle(up, 0.3f,groundLayer)&&groundDetection.localPosition!=(Vector3)down)
		{
			Directions.Add(1);
		}
			

		if (Physics2D.OverlapCircle(down, 0.3f, groundLayer) && groundDetection.localPosition != (Vector3)up) 
			Directions.Add(2);

		if (Physics2D.OverlapCircle(right, 0.3f, groundLayer) && groundDetection.localPosition != (Vector3)left)
			Directions.Add(3);
		if (Physics2D.OverlapCircle(left, 0.3f,groundLayer) && groundDetection.localPosition != (Vector3)right)
			Directions.Add(4);

		int direction = Directions[Random.Range(0, Directions.Count)];
		ChangeDirection(direction);
		if (direction == 1)
			groundDetection.localPosition = up;
		else if (direction == 2)
			groundDetection.localPosition = down;
		else if (direction == 3)
			groundDetection.localPosition = right;
		else
			groundDetection.localPosition = left;
	}
	public void ChangeDirection(int id)
	{

		if (id == 1)
			SetDirection(Vector2.up, spriteRendererUp);
		else if (id == 2)
			SetDirection(Vector2.down, spriteRendererDown);
		else if (id == 3)
			SetDirection(Vector2.right, spriteRendererRight);
		else
			SetDirection(Vector2.left, spriteRendererLeft);
	}
	public void EnemyAnimation()
	{
	  
		SetDirection(Vector2.left, spriteRendererLeft);
		
		/*transform.DOMoveX(-6.2f, 3f).SetEase(Ease.InOutSine).OnComplete(() =>
		{
			SetDirection(Vector2.right, spriteRendererRight);
			transform.DOMoveX(6.2f, 3f).SetEase(Ease.InOutSine);
			EnemyAnimation();
		});*/
		
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	  {
		  if (collision.gameObject.CompareTag("Player"))
		  {
			 


		  }
	  }

}
