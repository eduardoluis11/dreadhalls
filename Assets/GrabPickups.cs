using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrabPickups : MonoBehaviour {

	private AudioSource pickupSoundSource;

    /* Awake() function, which is like the init() function of a Love2D game.

    I get it: why should I care that the current level number should not be eliminated while resetting the scene?
    Because I don’t actually go to a new level each time that I pickup a purple coin: I just reset the current maze,
    and a new random maze is generated. That is accomplished by resetting the Play scene each time that I pick up a
    purple coin.

	So, knowing that, I need to print onscreen a counter that says the current level.

	To do that, I could simply add a counter. That counter would start at 1. That counter will be printed onscreen in
	the Play scene. Then once I pick up a purple coin, that counter would go up by one. So, once I pick up a purple
	coin and go to the next level, the counter would say “2” to indicate that I’m on level 2.

	So, the counter should be “1 + number of purple coins picked up”. That way, in level 1, the counter would say “1”;
	in level 2, the counter would say “1 + 1”, which, in this case, it’s “2”; and so on and so forth.

	I could begin by just printing the current level as a debug message in the console. After I make it work, I would
	later print it onscreen mid-level on the Play scene.

	YUP: the question says that the text should ONLY be displayed on the Play scene, NOT on the Game over scene, nor on
	the the Title scene.

	This counter should DEFINITELY be detecting stuff every frame during the Play scene (being checked 60 times each
	second). So, in which script should I declare the counter and print a debug message with its current value?

	Should it be on the FPSCharacterController.cs file? Should I create a new MonoBehaviour script and attach it to the
	FPSCharacterController game object? Where am I detecting if the player is colliding with the purple coins?
	Whenever I detect if the player collides with the purple coins, I should add that the level counter should go up by
	1.

	I suspect that I should create the counter in the GrabPickups.cs file. Inside the OnControllerColliderHit()
	function, I will update by 1 the counter. Then, on the Awake() function, which is pretty much the Unity equivalent
	of the init() function in Love2D and Lua, I will first declare the counter, and initially give it a value of 1.

    I forgot! I need to use static variables PRECISELY to prevent the variable from being reset while resetting the
    scene! That’s what the homework assignment was warning me! So, I will add the keyword “static” to my variable, and
    the currentLevel variable should stop from being reset back to 1!

    Oh! I need to make the currentLevel variable “public” instead of “private” so that I can use it as a global
    variable. That’s what the distro code did in the Helicopter Game assignment (source: my own Helicopter Game
    assignment from CS50: https://github.com/eduardoluis11/helicopter-gd50 )
    */

	// I will declare the counter which will keep track of the current level of the game (source: VS Code's Copilot.)
	// It needs to be "public" so that it turns into a "global variable" (source: my own Helicopter Game
    // assignment from CS50.)
	public static int currentLevel = 1;

	// This declares a boolean that will prevent the level counter from increasing by 2 (source: Copilot.)
	private bool levelIncreased;


	void Awake() {
		pickupSoundSource = DontDestroy.instance.GetComponents<AudioSource>()[1];

		// This boolean will make sure that the counter variable always goes up by 1 if you touch the purple coin
		// (source: Copilot.)
		levelIncreased = false;

		// // DEBUG: This will show the current level on the console
		// Debug.Log("Current Level: " + currentLevel);
	}

    /* This detects each time that the player collides with a purple coin.

    I will make it so that, right before the new level is generated (the Play scene is reset), the counter that
    keeps track of the current level will go up by one. That way, once I reach level 2, the ocunter will say "2"; "3"
    on level "3", and so on and so forth.

	I’ll modify the snippet from GrabPickups.cs so that the counter increases AFTER resetting the Play scene, so that the player stops touching 
	the coin by the time the counter goes up. This will prevent the level counter from increase more than 1 due to still being touching the purple 
	coin.
    */
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Pickup" && !levelIncreased) {
			pickupSoundSource.Play();

			// This will disable the purple coin to prevent a bug in which the level counter increases by 2 instead of 1
			//when I touch the coin. Supposedly, the coin should be enabled again when the player goes to the next level
			// (source: Copilot.)
            hit.gameObject.SetActive(false);



           // DEBUG: This will show the current level on the console
			Debug.Log("Current Level: " + currentLevel);

			// This will reset the maze to give you the illusion that you're going to a new level (the next level)
			SceneManager.LoadScene("Play");

			// This will increase the counter by 1 to indicate that you've reached the next level (the next maze)
			currentLevel += 1;

			// This boolean will make sure that the counter variable always goes up by 1 if you touch the purple coin
			// (source: Copilot.)
			levelIncreased = true;
		}
	}

	/* I have an idea! I will add an Update() function on the GrabPickups.cs file. In it, I will print the current value of the currentLevel
	counter. This will be printed 60 times every second. Then, I will eliminate the debug message from the OnColliderHit() function that shows
	the current level. That way, the correct current level will always be printed on the console. Sure, the level will be printed 60 times every
	second, but, at least it will be always the correct level.
	*/
	 void Update() {

	 	// DEBUG: This will show the current level on the console 60 times every second
	 	Debug.Log("Current Level: " + currentLevel);
	 }
}
