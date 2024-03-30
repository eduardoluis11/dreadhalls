using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

// This fixes the Unity error that says "The type or namespace name 'UI' could not be found"
using UnityEngine.UI;

// [RequireComponent(typeof (GUITexture))]

// BUGFIX: I needed to use "UnityEngine.UI.Image" instead of "GUITexture" or "UI.Image"
[RequireComponent(typeof (UnityEngine.UI.Image))]
public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // if we have forced a reset ...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            //... reload the scene
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }
}
