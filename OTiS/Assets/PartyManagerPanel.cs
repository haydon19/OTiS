using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManagerPanel : MonoBehaviour {

    public static PartyManagerPanel instance;
    // Use this for initialization
    void Start () {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);


    }

    public void openPartyManagerPanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if(gameObject.activeSelf)
        CharacterContainer.instance.populateContainer();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
