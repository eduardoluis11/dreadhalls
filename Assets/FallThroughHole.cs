using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* This script will make the player die if they fall through a hole.

If they fall below a certain height (a certain y value), a new scene called "Game Over" will be loaded.

For Debugging purposes, I could make a sample script: it would be similar to the script that makes you go to the next
level when you pick up the purple coin, but, instead of making you go to the next level when you pick up a purple coin,
it would send you to the next level when you fall through a hole (this is just for debugging purposes. I will later make
it so that, if you fall through a hole, you will get a “Game over” screen).

*/

public class FallThroughHole : MonoBehaviour {

    // This stores the player's character data. In the Unity Editor, I need to drag the Playable Character object (from
    // the left Side Navbar) into this variable.
	public GameObject characterController;

    // DEBUG: This will send the player to the next level if the fall through a hole
    void Update() {

        // If the player falls below the height of -10.
        // BUGGY: this didn't work.
        if (characterController.transform.position.y < -10) {

            // DEBUG: The player will be sent to the next level (I will later change this to a "Game Over" screen).
            SceneManager.LoadScene("Play");

            // DEBUG: this indicates that you fell through a hole
            Debug.Log("You fell through a hole. Game Over");
        }
    }

    //	private AudioSource pickupSoundSource;

    //	void OnControllerColliderHit(ControllerColliderHit hit) {
    //		if (hit.gameObject.tag == "Pickup") {
    //            //			pickupSoundSource.Play();
    //			SceneManager.LoadScene("Play");
    //		}
    //	}


}
