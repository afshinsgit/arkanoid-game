using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour {

    public int brickLife;
    public Transform particalBreak;
    public GameManager gm;
    public int points;
    public Transform powerUp;

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        int rand = Random.Range(1, 101);

        if (rand < 15)
        {
            Instantiate(powerUp, collision.transform.position, collision.transform.rotation);
        }

        brickLife--;

        if (collision.transform.CompareTag("Ball") && brickLife <= 0)
        {
            Destroy(gameObject);
            Transform newParticalBreak = Instantiate(particalBreak, collision.transform.position, collision.transform.rotation);
            Destroy(newParticalBreak.gameObject, 1.5f);        
        }
    }
}
