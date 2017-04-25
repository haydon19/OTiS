using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public StartMenuScript StartMenu;

    public void OpenStartMenu()
    {
        StartMenu.gameObject.SetActive(!StartMenu.gameObject.activeSelf);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
