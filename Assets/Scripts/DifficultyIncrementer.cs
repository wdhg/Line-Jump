using UnityEngine;
using System.Collections;

public class DifficultyIncrementer : MonoBehaviour {

    public float change, rate;

    private Spawner spawner;
    private float prevTime;

    void Awake () {
        spawner = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<Spawner> ();
    }

    void MakeHarder () {
        if (spawner.spawnTime > spawner.minSpawnTime) {
            spawner.spawnTime /= change;
        }
    }

    void FixedUpdate () {
        if (prevTime + rate < Time.time) {
            MakeHarder ();
            prevTime = Time.time;
        }
    }

}