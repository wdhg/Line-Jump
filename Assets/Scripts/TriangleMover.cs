using UnityEngine;
using System.Collections;

public class TriangleMover : MonoBehaviour {

    private float speed = 7f;
    private PlayerController pc;

    void Awake () {
        pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
    }

    void Move () {
        transform.Translate (new Vector2 (-speed * Time.deltaTime, 0f));
        if (transform.position.x < -10f) {
            Destroy (transform.gameObject);
        }
    }

    void FixedUpdate () {
        if (pc.alive) {
            Move ();
        } else {
            Destroy (transform.gameObject);
        }
    }
}
