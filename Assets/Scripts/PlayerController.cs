using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float jumpForce, gravityScale, speed;
    public GameObject gameOverScreen, menuScreen, distanceText; // distanceText is a GameObject so it can be disabled easily
    public Text endDistanceText; // So we can set it to the distance variable
    [System.NonSerialized] // Hide it from the unity inspector
    public bool alive;

    private Vector2 translation;
    // Need this as otherwise it doesn't work
    private bool isPhoneApp = Application.platform == RuntimePlatform.Android;
    private float distance;

    void Start () {
        alive = false; // Start of dead, so the menu can be shown
        ToggleText (); // This hides the game over screen 
    }

    void ToggleText () {
        gameOverScreen.SetActive (!gameOverScreen.activeSelf);
        distanceText.SetActive(!distanceText.activeSelf);
    }

    void ToggleAlive () {
        alive = !alive;
        endDistanceText.text = "DISTANCE: " + System.Math.Round(distance, 2).ToString() + "m";
        distance = 0f;
    }

    void Move () {
        if (-.2f < transform.position.y && transform.position.y < .2f) {
            transform.position = Vector2.zero;
            translation = Vector2.zero;
        }
        if (alive) {
            distance += speed * Time.deltaTime;
            distanceText.GetComponent<Text>().text = ((int) distance).ToString() + "m";
            if (transform.position.y == 0f) {
                Touch touch;
                if (isPhoneApp) {
                    touch = Input.GetTouch (0);
                } else {
                    touch = new Touch ();
                }

                if (Input.GetKey (KeyCode.UpArrow) || (isPhoneApp && touch.position.x < Screen.width / 2)) {
                    translation.y = jumpForce;
                } else if (Input.GetKey (KeyCode.DownArrow) || (isPhoneApp && touch.position.x > Screen.width / 2)) {
                    translation.y = -jumpForce;
                }
            }
        }
        translation -= (Vector2) (transform.position * gravityScale * Time.deltaTime);
        transform.Translate (translation);
    }

    // The player has lost
    void OnTriggerEnter2D (Collider2D other) {
        ToggleAlive ();
        ToggleText ();
    }

    void FixedUpdate () {
        Move ();
    }

    void Update () {
        // Long if statment. Checks if player is alive, the menu is disabled, and for a space press / tap by the user
        // Probs change in the future to a more neat solution
        if ((!alive && !menuScreen.activeSelf) && 
            (Input.GetKeyDown (KeyCode.Space)  || (isPhoneApp && Input.touches[0].phase == TouchPhase.Began))) {
            ToggleAlive ();
            ToggleText ();
        }
    }
}
