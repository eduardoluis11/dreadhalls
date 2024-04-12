using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnInput : MonoBehaviour {

	// // I will declare the counter which will keep track of the current level of the game (source: VS Code's Copilot)
	// private static int currentLevel = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame.
	/* I modified this function so that, if the current scene is the Press Start scene (the "Title" scene), you will
	be sent to the Play scene (the maze scene) if you press Enter; meanwhile, if the current scene is the Game Over
	scene, you will be sent to the Press Start scene (the "Title" scene) if you press Enter.

	I want to constantly be checking the game until I detect if the player has pressed “Enter” in the Game Over screen,
	in which case, I need to destroy the song (the whispers), AND THEN change to the “Title” scene. Come to think of it,
	why should I do this on the DontDestroy.cs script? Why don’t I put this on the script that changes the scene from
	the Game Over scene to the Title scene (this script)?

    To destroy a game object, I need to use:
        Destroy(gameObject);

    The whispers are stored in a variable / file called WhisperSource. This is the Game Object / song that I need to
    destroy when going from the Game Over scene to the Press Start scene.

	*/
	void Update () {
		if (Input.GetAxis("Submit") == 1) {

            string currentScene = SceneManager.GetActiveScene().name;

            // If the current scene is the Game Over scene
			if (currentScene == "GameOver") {

				// This should find the file with the whispers in the game, then, it will insert it into this variable
				// (source: Copilot from VS Code)
				GameObject WhisperSource = GameObject.Find("WhisperSource");

				// This should stop the whispers from playing and eliminate it before going to the Title scene
				Destroy(WhisperSource);

                 // This resets the level back to 0 if the player dies, so that they start once again from level 1
                GrabPickups.currentLevel = 1;
			    // currentLevel = 0;

				// This sends the player to the Title scene
				SceneManager.LoadScene("Title");

			} else if (currentScene == "Title") {

                // // This will increase the counter by 1 to indicate that you've reached the next level (the next maze)
			    // currentLevel += 1;

				SceneManager.LoadScene("Play");
			}

        //			SceneManager.LoadScene("Play");
		}

        // // DEBUG: This will show the current level on the console 60 times every second
	 	// Debug.Log("Current Level: " + currentLevel);
	}
}
