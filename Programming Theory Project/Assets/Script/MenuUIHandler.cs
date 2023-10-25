using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    // ABSTRACTION - A higher-level method to start a new game and load a scene.
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    // ABSTRACTION - A higher-level method to exit the game.
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode(); // Exiting play mode in the Unity Editor.
#else
        Application.Quit(); // Exiting the standalone player (build).
#endif
    }
}
