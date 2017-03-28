using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundControllerScript : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip backgroundMusic;
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
           // DontDestroyOnLoad(gameObject);
        }
        gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.GetComponent<AudioSource>();
        musicSource.clip = backgroundMusic;

    }



    // Update is called once per frame
    void Update()
    {
        if (!(musicSource.isPlaying))
        {
            musicSource.Play();
        }
        else
        {
            //Debug.log("Something is wrong with Music.");
        }
    }


}