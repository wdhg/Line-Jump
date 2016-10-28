using UnityEngine;
using System.Collections;


[System.Serializable]
public class Hazard {
    public GameObject hazardObject;
    public int lineCount;
    public float spawnChance;
}

public class Spawner : MonoBehaviour {

    public Hazard triangle;
    public Hazard square;
    public float maxMinTime, maxRandTime;
    [System.NonSerialized]
    public float prevTime = 0f, randTime, minTime;

    private PlayerController pc;
    private float highestYPoint = 5f;
    private float totalChance;

    void Awake () {
        pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
        totalChance += triangle.spawnChance + square.spawnChance;
        minTime = maxMinTime;
    }

    bool RandomBool () {
        return Random.value > 0.5f;
    }

    // Takes position because it won't just be spawned at the spawner's location
    public void MakeTriangle (Vector2 pos) {
        GameObject newTriangle = (GameObject) Instantiate (triangle.hazardObject,
            pos,
            triangle.hazardObject.transform.rotation);
    }

    void MakeTriangleLine (bool upwards, int number) {
        if (number > triangle.lineCount) {
            return;
        }
        GameObject[] newTriangles = new GameObject[number];
        for (int index = 0; index < number; index++) {
            float yPos;
            if (upwards) {
                yPos = index * (highestYPoint / number);
            } else {
                yPos = -index * (highestYPoint / number);
            }
            newTriangles[index] = (GameObject) Instantiate (triangle.hazardObject,
                new Vector2 (transform.position.x, yPos),
                triangle.hazardObject.transform.rotation);
        }
    }

    void MakeSquares () {
        GameObject[] squares = new GameObject[square.lineCount];
        for (int index = 0; index < square.lineCount; index++) {
            squares[index] = (GameObject) Instantiate (square.hazardObject,
                new Vector2 (transform.position.x + index, 0f),
                square.hazardObject.transform.rotation);
        }
    }

    void SpawnHazard () {
        // Random chance number
        float chance = Random.Range (0f, totalChance);
        if (chance <= triangle.spawnChance) { // Spawn triangle
            // Random bool
            if (RandomBool ()) {
                MakeTriangle (transform.position);
            } else {
                MakeTriangleLine (RandomBool (), triangle.lineCount);
            }
        } else if (chance <= triangle.spawnChance + square.spawnChance) {
            MakeSquares ();
        }
    }

    void Update () {
        if (pc.alive && prevTime + minTime + randTime < Time.time) {
            prevTime = Time.time;
            randTime = Random.Range (0f, maxRandTime);
            SpawnHazard ();
        } else if (!pc.alive) {
            minTime = maxMinTime;
        }
    }
}