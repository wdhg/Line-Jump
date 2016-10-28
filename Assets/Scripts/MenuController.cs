using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    private PlayerController pc;
    private bool isPhoneApp = Application.platform == RuntimePlatform.Android;

    void Awake () {
        pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.Space) || (isPhoneApp && Input.touches[0].phase == TouchPhase.Began)) {
            pc.alive = true;
            pc.distanceText.SetActive (true);
            transform.gameObject.SetActive (false);
        }
    }
}