using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    private float dropSpeed = 5f;

	void Update()
    {
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * dropSpeed);

        if (transform.position.y < -6.5)
        {
            Destroy(gameObject);
        }
    }
}