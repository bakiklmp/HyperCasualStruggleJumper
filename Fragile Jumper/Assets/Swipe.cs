using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Swipe : MonoBehaviour
{
    [Header("Swipe")]
    [SerializeField] private float minDistance = .2f;
    [SerializeField] private float maxTime = 1f;
    [SerializeField] private float swipeForce;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    //public ParticleSystem dust;

    private Rigidbody2D playerRb;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Fall Damage")]
    [SerializeField] private float fallThreshold = 3f;
    [SerializeField]private float offScreenDistance = 3f;

    private void OnEnable()
    {
        InputManager.OnStartTouch += SwipeStart;
        InputManager.OnEndTouch += SwipeEnd;
    }
    private void OnDisable()
    {
        InputManager.OnStartTouch -= SwipeStart;
        InputManager.OnEndTouch -= SwipeEnd;
    }
    private void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;        
        startTime = time;
    }
    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;        
        endTime = time;
        DetectSwipe();
    }

    private void Update()
    {
        bool previousGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);        
        if (!previousGrounded && isGrounded)
        {
            if(playerRb.velocity.y < -fallThreshold)
            {
                GameOver();
            }
            
        }


        /*if (!isGrounded)
        {
            if(transform.position.y< previousPosition.y && firstTime)
            {
                firstTime = false;
                isFalling = true;
                highestPosition = transform.position.y;
            }
            previousPosition = transform.position;
        }

        if(isGrounded && isFalling)
        {
            if (highestPosition - transform.position.y > fallThreshold)
            {
                GameOver();
            }
            isFalling = false;
            firstTime = true;
        }*/
        if (playerRb.velocity.y > 1)
        {
            CreateDust();
        }
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minDistance &&
            (endTime - startTime) <= maxTime)
        {
            Vector3 direction = endPosition - startPosition;

            if (isGrounded)
            {                
                playerRb.AddForce(direction * swipeForce, ForceMode2D.Impulse);

            }

        }

    }
    private void OnBecameInvisible()
    {
        Invoke("GameOver", 0.6f);
      
    }
    void CreateDust()
    {
       // dust.Play();
    }
}

