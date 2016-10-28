using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    private PlayerController pc; // So the player can set active
    private bool isPhoneApp = Application.platform == RuntimePlatform.Android;

    void Awake () {
        pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.Space) || (isPhoneApp && Input.touches[0].phase == TouchPhase.Began)) {
            pc.alive = true;
            // I call this here because it needs to be inversed to the game over screen and has to be shown after the menu screen
            pc.distanceText.SetActive (true);
            transform.gameObject.SetActive (false); // Disables the menu screen
        }
    }
}