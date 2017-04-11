using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public static GameData instance;
    public Character player1;
    public static int characterIndex;
    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            //...set this one to be it...
            instance = this;
            DontDestroyOnLoad(gameObject);
            //...otherwise...
        }
        else if (instance != this)
        {
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        }
    }


    public int nextCharID()
    {
        return characterIndex += 1;
    }


}
