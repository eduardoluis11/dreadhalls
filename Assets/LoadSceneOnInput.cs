using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame.
	/* I modified this function so that, if the current scene is the Press Start scene (the "Title" scene), you will
	be sent to the Play scene (the maze scene) if you press Enter; meanwhile, if the current scene is the Game Over
	scene, you will be sent to the Press Start scene (the "Title" scene) if you press Enter.
	*/
	void Update () {
		if (Input.GetAxis("Submit") == 1) {

            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "GameOver") {
                SceneManager.LoadScene("Title");
            } else if (currentScene == "Title") {
                SceneManager.LoadScene("Play");
            }

        //			SceneManager.LoadScene("Play");
		}
	}
}
