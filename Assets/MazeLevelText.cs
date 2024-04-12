/* Script that will print the current level (the current maze) onscreen.

Source of most of this code: my own code from my submission for the Helicopter Game assignment from GD50:
https://github.com/eduardoluis11/helicopter-gd50/blob/main/Assets/Resources/Scripts/CoinText.cs .
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]

/* Let’s not reinvent the wheel: I already have the counter with the current Maze level. So, I’ll just use that as a
global variable, and print it on the new script that I created to show the text onscreen during the Play scene with the
current level.

After looking at how the Coin text is being displayed in the Unity editor for the Helicopter Assignment, I noticed
that I’ll have to make a Canvas Game Object, just like in the Title and the Game Over scene. Then’ I’ll have to add
some text like “Current level”. Then, I’ll have to attach the MazeLevelText.cs script to that Canvas Game Object.

Actually, never mind: I don’t have to write anything on the “Text:” option of the Unity editor: the text there gets
automatically typed / written. Probably the CoinText.cs, with the “text” property, automatically types what should be
written in there so that it writes the correct number of coins. So, I'll have to do something similar to print
the current level onscreen.

*/
public class MazeLevelText : MonoBehaviour {

    //	public GameObject helicopter;

    //    // This gets the current level. This is obtained from the GrabPickups.cs script
    //    // (source: my own code from my submission for GD50's Helicopter Game assignment.)
    //    GrabPickups.currentLevel = 1;

	private Text text;
    //	private int coins;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
        //		if (helicopter != null) {
        //			coins = helicopter.GetComponent<HeliController>().coinTotal;
        //		}
		text.text = "Current Level: " + GrabPickups.currentLevel;
	}
}