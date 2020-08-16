using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

    public float screenEdgeLeft; // invisible wall for the left side of the screen
    public float screenEdgeRight; // invisible wall for the right side of the screen
    public GameManager gm;
    AudioSource audioClip;

    [SerializeField]
    private float paddleSpeed;
    
    void Start ()
    {
        audioClip = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        BlockScreenEdges();
        PaddleMovement();
    }

    void PaddleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * paddleSpeed);

        if (gm.ballLives == 0)
        {
            paddleSpeed = 0;
        }
    }

    // Blockes the player from going over the side of the screen 
    void BlockScreenEdges()
    {
        // Right edge
        if (transform.position.x > screenEdgeRight)
        {
            transform.position = new Vector2(screenEdgeRight, transform.position.y);
        }
        // Left edge
        if (transform.position.x < screenEdgeLeft)
        {
            transform.position = new Vector2(screenEdgeLeft, transform.position.y);
        }
    }

    // Updates player's status for each Power-Up
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ExtraLifePowerUp"))
        {
            gm.UpdateLives(1);
            Destroy(collision.gameObject);
            audioClip.Play();
        }

        if (collision.CompareTag("ScaleUpPowerUp"))
        {
            StartCoroutine(ScaleUpPowerUp());
            StopCoroutine(ScaleUpPowerUp());

            Destroy(collision.gameObject);
            audioClip.Play();
        }

        if (collision.CompareTag("ScaleDownPowerUp"))
        {
            StartCoroutine(ScaleDownPowerUp());
            StopCoroutine(ScaleDownPowerUp());

            Destroy(collision.gameObject);
            audioClip.Play();
        }

        if (collision.CompareTag("FastPowerUp"))
        {
            StartCoroutine(FastSpeedPowerUp());
            StopCoroutine(FastSpeedPowerUp());

            Destroy(collision.gameObject);
            audioClip.Play();
        }

        if (collision.CompareTag("SlowPowerUp"))
        {
            StartCoroutine(SlowSpeedPowerUp());
            StopCoroutine(SlowSpeedPowerUp());

            Destroy(collision.gameObject);
            audioClip.Play();
        }
    }
    
    // Power ups dropping from the bricks when player breaks them
    // (Power up) Player scales up for 5 seconds
    IEnumerator ScaleUpPowerUp()
    {
        transform.localScale = new Vector3(0.35f, 0.2323824f, 0.3199008f);      
        yield return new WaitForSeconds(5f);
        transform.localScale = new Vector3(0.2446249f, 0.2323824f, 0.3199008f);
        
    }
    // (Power up) Player scales down for 5 seconds
    IEnumerator ScaleDownPowerUp()
    {
        transform.localScale = new Vector3(0.15f, 0.2323824f, 0.3199008f);
        yield return new WaitForSeconds(5f);
        transform.localScale = new Vector3(0.2446249f, 0.2323824f, 0.3199008f);
    }
    // (Power up) Player speeds up for 5 seconds
    IEnumerator FastSpeedPowerUp()
    {
        paddleSpeed += 15f;
        yield return new WaitForSeconds(5f);
        paddleSpeed = 15;
    }
    // (Power up) Player slows down for 5 seconds
    IEnumerator SlowSpeedPowerUp()
    {
        paddleSpeed -= 7f;
        yield return new WaitForSeconds(5f);
        paddleSpeed = 15;
    }
}