using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuScript : MonoBehaviour {

    public static StartMenuScript instance;
    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }
}
