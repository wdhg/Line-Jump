using UnityEngine;
using System.Collections;

public class DiamondMover : MonoBehaviour {

    private float speed = 1f;
    private Spawner spawner;

    void Awake () {
        spawner = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<Spawner>();
    }

    void Move () {
        transform.Translate (new Vector2 (-speed, 1f) * Time.deltaTime);
    }

    void FixedUpdate () {
        Move ();
    }
}
