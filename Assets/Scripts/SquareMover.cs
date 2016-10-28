using UnityEngine;
using System.Collections;

public class SquareMover : MonoBehaviour {

    private float speed = 7f;
    // How far it will oscillate
    private float vertDistance = 5f;
    private float vertSpeed;
    private PlayerController pc;

    void Awake () {
        vertSpeed = (Random.value > 0.5f) ? speed : -speed;
        pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
    }

    void Move () {
        if (transform.position.y > vertDistance) {
            vertSpeed = -speed;
        } else if (transform.position.y < -vertDistance) {
            vertSpeed = speed;
        }

        transform.Translate (new Vector2 (-speed, vertSpeed * 2) * Time.deltaTime);
    }

    void FixedUpdate () {
        if (pc.alive) {
            Move ();
        } else {
            Destroy (transform.gameObject);
        }
    }
}
