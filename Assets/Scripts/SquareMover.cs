using UnityEngine;
using System.Collections;

public class SquareMover : MonoBehaviour {

    // How far it will oscillate
    private float vertDistance = 5f;
    private float vertSpeed;
    private PlayerController pc;

    void Awake () {
        vertSpeed = (Random.value > 0.5f) ? pc.speed : -pc.speed;
        pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
    }

    void Move () {
        if (transform.position.y > vertDistance) {
            vertSpeed = -pc.speed;
        } else if (transform.position.y < -vertDistance) {
            vertSpeed = pc.speed;
        }

        transform.Translate (new Vector2 (-pc.speed, vertSpeed * 2) * Time.deltaTime);
    }

    void FixedUpdate () {
        if (pc.alive) {
            Move ();
        } else {
            Destroy (transform.gameObject);
        }
    }
}
