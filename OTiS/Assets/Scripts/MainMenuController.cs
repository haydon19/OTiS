using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//public enum Screen { MainMenuScreen, CharacterScreen }

public class MainMenuController : MonoBehaviour {

    MainMenuController instance;
    public List<GameObject> screens;
    int activeScreen;
	// Use this for initialization
	void Start () {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

        activeScreen = 0;
        setActiveScreen();
        //SoundControllerScript.instance.PlayMusic(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void ChangeScreen(int screen)
    {
        activeScreen = screen;
        setActiveScreen();

    }

    public void setActiveScreen()
    {
        for(int i = 0; i < screens.Count; i++)
        {

            if(i == activeScreen)
            {
                screens[i].SetActive(true);
                screens[i].transform.position = transform.position;

            } else
            {
                screens[i].SetActive(false);

            }
        }
    }
	
}
