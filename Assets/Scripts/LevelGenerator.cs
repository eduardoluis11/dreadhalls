using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public GameObject floorPrefab;
	public GameObject wallPrefab;
	public GameObject ceilingPrefab;

	public GameObject characterController;

	public GameObject floorParent;
	public GameObject wallsParent;

	// allows us to see the maze generation from the scene view
	public bool generateRoof = true;

	// number of times we want to "dig" in our maze
	public int tilesToRemove = 50;

	public int mazeSize;

	// spawns at the end of the maze generation
	public GameObject pickup;

	// this will determine whether we've placed the character controller
	private bool characterPlaced = false;

	// 2D array representing the map
	private bool[,] mapData;

	// we use these to dig through our maze and to spawn the pickup at the end
	private int mazeX = 4, mazeY = 1;

	// This is the max number of holes allowed per maze.
    int numberOfHoles = 4;

    // This keeps track of how many holes have been created.
    int holesCreated = 0;

	// Use this for initialization.
	/* This is what, among other things, renders the floor.

	I'll edit this function so that there are at least 3 or 4 holes in the ground.

	TEMPORARY SOLUTION: If I simply say that there's a 5% chance of creating a hole in the floor, without specifying
	the maximum number of hoels that should be created, then only some holes are created in the floor. This temporarily
	fixes the bug which either rendered the entire floor, or didn't render the floor at all.

	I'm now specifying the maximum number of holes that should be created in the "if" statement that creates the hole
	in the floor 5% of the time. That is, I'm using "&& holesCreated < 4" instead of "&& holesCreated < numberOfHoles",
	and now no more than 4 holes are created in the floor. HOWEVER, sometimes, only 2 or 3 hole are created per maze.
	I wish I could guarantee that there are at least 4 holes per maze. Maybe increasing the chances of creating a hole
	in the floor to a percent higher than 5% should do the trick, although it's not the most elegant solution.

	I added a console message showing the number of holes rendered per maze to show the player that, indeed, there
	are 4 holes rendered per level. It's sometimes hard to find all the 4 holes in a single maze because it's
	easy to get lost in the mazes, so it's hard to find all of the holes in a single level. However, I explored like
	5 mazes, and the debug console always ended up talling me that at least 4 holes were created in the floor in
	each maze / level. So please, check the console in your unity editor to make sure that 4 holes were rendered
	in the current maze.
    */
	void Start () {

		// initialize map 2D array
		mapData = GenerateMazeData();

		// create actual maze blocks from maze boolean data
		for (int z = 0; z < mazeSize; z++) {
			for (int x = 0; x < mazeSize; x++) {
				if (mapData[z, x]) {
					CreateChildPrefab(wallPrefab, wallsParent, x, 1, z);
					CreateChildPrefab(wallPrefab, wallsParent, x, 2, z);
					CreateChildPrefab(wallPrefab, wallsParent, x, 3, z);
				} else if (!characterPlaced) {
					
					// place the character controller on the first empty wall we generate
					characterController.transform.SetPositionAndRotation(
						new Vector3(x, 1, z), Quaternion.identity
					);

					// flag as placed so we never consider placing again
					characterPlaced = true;
				}

                // This is a randon number generator that will determine if a hole should be created (source: Copilot)
                float randomChance = Random.value;

                // This has a 5% chance of creating a hole in the floor if the number of holes created is less than the
                // max number of holes allowed (source: Copilot.) That is, if the randomChance variable gives a number
                // of less than 0.05, this will create a hole.
                //                if (randomChance < 0.95f && holesCreated < numberOfHoles) {
                //                    // This will add 1 to the counter that keeps track of the total number of holes created.
                //                    holesCreated++;

                // This has a 5% chance of creating a hole in the floor, regardless of the number of holes created.
                // I'm specifying right here the maximum number of holes to prevent the bug that either renders the
                // entire floor, or doesn't render the floor at all.
                if (randomChance < 0.05f && holesCreated < 4) {
                    // This will add 1 to the counter that keeps track of the total number of holes created.
                    holesCreated++;

                    // DEBUG: This will print the number of holes created to the console
                    Debug.Log("A hole was created. Total number of holes: " + holesCreated);
                } else {
                    // This renders the floor by rendering a block for a tile for the floor.
                    CreateChildPrefab(floorPrefab, floorParent, x, 0, z);
                }

                //                // Instead of an "else", I will use another "if" to render the remaining tiles for the floor.
                //                if (holesCreated < numberOfHoles || randomChance >= 0.95f) {
                //                    CreateChildPrefab(floorPrefab, floorParent, x, 0, z);
                //                }



                //				// create floor and ceiling.
                //				/* I THINK this is the line of code I need to edit to spawn the holes in the floor. */
                //				CreateChildPrefab(floorPrefab, floorParent, x, 0, z);

				if (generateRoof) {
					CreateChildPrefab(ceilingPrefab, wallsParent, x, 4, z);
				}
			}
		}

		// spawn the pickup at the end
		var myPickup = Instantiate(pickup, new Vector3(mazeX, 1, mazeY), Quaternion.identity);
		myPickup.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
	}

	// generates the booleans determining the maze, which will be used to construct the cubes
	// actually making up the maze
	bool[,] GenerateMazeData() {
		bool[,] data = new bool[mazeSize, mazeSize];

		// initialize all walls to true
		for (int y = 0; y < mazeSize; y++) {
			for (int x = 0; x < mazeSize; x++) {
				data[y, x] = true;
			}
		}

		// counter to ensure we consume a minimum number of tiles
		int tilesConsumed = 0;

		// iterate our random crawler, clearing out walls and straying from edges
		while (tilesConsumed < tilesToRemove) {
			
			// directions we will be moving along each axis; one must always be 0
			// to avoid diagonal lines
			int xDirection = 0, yDirection = 0;

			if (Random.value < 0.5) {
				xDirection = Random.value < 0.5 ? 1 : -1;
			} else {
				yDirection = Random.value < 0.5 ? 1 : -1;
			}

			// random number of spaces to move in this line
			int numSpacesMove = (int)(Random.Range(1, mazeSize - 1));

			// move the number of spaces we just calculated, clearing tiles along the way
			for (int i = 0; i < numSpacesMove; i++) {
				mazeX = Mathf.Clamp(mazeX + xDirection, 1, mazeSize - 2);
				mazeY = Mathf.Clamp(mazeY + yDirection, 1, mazeSize - 2);

				if (data[mazeY, mazeX]) {
					data[mazeY, mazeX] = false;
					tilesConsumed++;
				}
			}
		}

		return data;
	}

	// allow us to instantiate something and immediately make it the child of this game object's
	// transform, so we can containerize everything. also allows us to avoid writing Quaternion.
	// identity all over the place, since we never spawn anything with rotation
	void CreateChildPrefab(GameObject prefab, GameObject parent, int x, int y, int z) {
		var myPrefab = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
		myPrefab.transform.parent = parent.transform;
	}
}
