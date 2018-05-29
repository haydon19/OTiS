using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManagerPanel : MonoBehaviour {

    public static PartyManagerPanel instance;
    public GameObject panelHolder;
    public int currTab = 0;
    // Use this for initialization
    void Awake() {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

        currTab = 0;
    }

    private void Start()
    {


    }
    public void openPartyManagerPanel()
    {
        
        gameObject.SetActive(!gameObject.activeSelf);
        gameObject.transform.position = new Vector3(0, 0, 0);
        GameControllerScript.instance.pauseGame();
        if (gameObject.activeSelf)
        {
            openTab(0);
        }

    }
    
    public void openTab(int tabID)
    {

        if(tabID != currTab)
        closeTab(currTab);

        switch (tabID)
        {
            case (0):
                
                CharacterInfoPanel.instance.gameObject.SetActive(true);
                //CharacterInfoPanel.instance.gameObject.transform.position = panelHolder.transform.position;

                CharacterContainer.instance.populateContainer();
                CharacterInfoPanel.instance.ActiveCharacter = GameControllerScript.instance.party.partyMembers[0];
                currTab = tabID;
                break;
            case (1):
                ShipInfoPanel.instance.gameObject.SetActive(true);
               // ShipInfoPanel.instance.gameObject.transform.position = panelHolder.transform.position;
                //ShipInfoPanel.instance.CurrentShip = GameControllerScript.instance.party.ship;
                currTab = tabID;
                break;

        }

    }

    public void closeTab(int tabID)
    {
        switch (tabID)
        {
            case (0):
                CharacterInfoPanel.instance.gameObject.SetActive(false);

                break;
            case (1):
                ShipInfoPanel.instance.gameObject.SetActive(false);

                break;

        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
