using UnityEngine;
using System.Collections;

// Every object that kills the player is a hazard
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
    [System.NonSerialized] // Hide from unity inspector
    public float prevTime = 0f, randTime, minTime;

    private PlayerController pc;
    private float highestYPoint = 5f; // The highest y point that the triangle objects can be spawned at
    private float totalChance; // The total of all the hazards chance values

    void Awake () {
        pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
        totalChance += triangle.spawnChance + square.spawnChance;
        minTime = maxMinTime;
    }

    bool RandomBool () {
        return Random.value > 0.5f;
    }

    // Takes position because it won't just be spawned at the spawner's location. Will change this to return a triangle object
    public void MakeTriangle (Vector2 pos) {
        GameObject newTriangle = (GameObject) Instantiate (triangle.hazardObject,
            pos,
            triangle.hazardObject.transform.rotation);
    }

    void MakeTriangleLine (bool upwards, int number) {
        // Will remove this
        if (number > triangle.lineCount) {
            return;
        }
        GameObject[] newTriangles = new GameObject[number];
        for (int index = 0; index < number; index++) {
            float yPos;
            // If the line should go upwards or downwards
            if (upwards) {
                yPos = index * (highestYPoint / number);
            } else {
                yPos = -index * (highestYPoint / number);
            }
            // Will change this to call MakeTriangle instead
            newTriangles[index] = (GameObject) Instantiate (triangle.hazardObject,
                new Vector2 (transform.position.x, yPos),
                triangle.hazardObject.transform.rotation);
        }
    }

    // Need to make a MakeSquare function for this to call
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
        } else if (chance <= triangle.spawnChance + square.spawnChance) { // Spawn square
            MakeSquares ();
        }
    }

    void Update () {
        // If the player is alive and enough time has elapsed since the last spawn
        if (pc.alive && prevTime + minTime + randTime < Time.time) {
            prevTime = Time.time;
            randTime = Random.Range (0f, maxRandTime);
            SpawnHazard ();
        } else if (!pc.alive) { // When the player is dead
            minTime = maxMinTime;
        }
    }
}