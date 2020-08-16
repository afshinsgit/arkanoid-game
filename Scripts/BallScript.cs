using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    private Rigidbody2D rb;
    public float ballSpeed;
    public bool ballMoving;
    public GameManager gm;
    public Transform ballPos;
    public float addSpeed;
    AudioSource audioClip;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        audioClip = GetComponent<AudioSource>();
	}

	void Update()
    {
        MoveBall();
        DefaultBallPosition();
    }

    void MoveBall()
    {
        if (gm.gameOver == false)
        {
            if (Input.GetButtonDown("Jump") && ballMoving == false)
            {
                ballMoving = true;
                rb.AddForce(Vector2.up * ballSpeed);
            }
        }
    }

    void DefaultBallPosition()
    {
        if (ballMoving == false)
        {
            transform.position = ballPos.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            rb.velocity = (Vector2.zero);
            ballMoving = false;
            gm.UpdateLives(-1);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Paddle" && ballMoving == true)
        {
            rb.AddForce(rb.velocity * addSpeed, ForceMode2D.Force);
            audioClip.Play();
        }

        // Checks and updates the number of bricks left durning the gameplay
        if (other.transform.CompareTag("Brick"))
        {
            gm.UpdateNumberOfBricks();
            gm.UpdateScore(other.gameObject.GetComponent<BrickScript>().points);
            audioClip.Play();
        }

        // Play sound when ball collides with the wall
        if (other.transform.CompareTag("Wall"))
        {
            audioClip.Play();
        }

    }
}

