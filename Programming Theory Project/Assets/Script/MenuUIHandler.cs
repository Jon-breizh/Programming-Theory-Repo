using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    // ENCAPSULATION - variable declaration
    public GameObject OptionPanel;
    [SerializeField] private Slider mainVolumeSlider, effectVolumeSlider;

    AudioSource audioMenu;

    private void Start()
    {
        audioMenu = GetComponent<AudioSource>();
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode(); // Exiting play mode in the Unity Editor.
#else
        Application.Quit(); // Exiting the standalone player (build).
#endif
    }

    // Option panel Management
    public void ShowOption()
    {
        OptionPanel.transform.position = new Vector3(122.5f, 175f,1);
    }

    //Put the panel outside the screen and save the change
    public void HideOption()
    {
        GameManager.Instance.mainVolumeValue = mainVolumeSlider.value;
        audioMenu.volume = mainVolumeSlider.value;
        GameManager.Instance.effectVolumeValue = effectVolumeSlider.value;
        OptionPanel.transform.position = new Vector3(-122.5f, 175f, 1);
    }
}
