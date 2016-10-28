using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float jumpForce, gravityScale;
    public GameObject gameOverScreen, menuScreen, distanceText;
    public Text endDistanceText;
    [System.NonSerialized]
    public bool alive;

    private Vector2 translation;
    // Need this as otherwise it doesn't work
    private bool isPhoneApp = Application.platform == RuntimePlatform.Android;
    private float distance;

    void Start () {
        alive = false;
        ToggleText ();
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
            distance += 3.5f * Time.deltaTime;
            distanceText.GetComponent<Text>().text = ((int) distance).ToString() + "m";
            if (transform.position.y == 0f) {
                // Need to set a global speed variable. Right now everything is using 7f per second
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

    // Lost
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
