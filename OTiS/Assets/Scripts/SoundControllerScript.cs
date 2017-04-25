using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SoundControllerScript : MonoBehaviour
{
    public AudioSource musicSource;
    public List<AudioClip> backgroundMusic;
    public static SoundControllerScript instance = null;


    // Use this for initialization
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.GetComponent<AudioSource>();

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene previousScene, Scene newScene)
    {
        if (instance != this)
            return;
            //AudioSource music = new AudioSource();
            //musicSource.Stop
            musicSource.clip = backgroundMusic[newScene.buildIndex];
             musicSource.loop = true;
             musicSource.Play();
    }

    


}