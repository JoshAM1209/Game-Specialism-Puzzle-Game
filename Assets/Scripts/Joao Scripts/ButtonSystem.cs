using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonSystem : MonoBehaviour
{

    public GameObject menu;
    public GameObject options;
    public GameObject playerButton, optionButton, quitButton;
    public GameObject graphicButton, colourButton, maxVolumeSlider, musicVolumeSlider, vfxVolumeSlider, exitButtom, vfxButton;
    // Start is called before the first frame update
    void Start()
    {
        MainScene();
        KeyboardControlls1();
    }
    public void MainScene()
    {

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OptionsgMenu()
    {
        options.SetActive(true);
        KeyboardControlls2();
    }
    public void KeyboardControlls1()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playerButton);
    }
    public void KeyboardControlls2()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(graphicButton);
    }
    public void OptionsgExit()
    {
        options.SetActive(false);
        KeyboardControlls1 ();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
