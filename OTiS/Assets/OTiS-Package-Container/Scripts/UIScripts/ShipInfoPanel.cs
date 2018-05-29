using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ShipInfoPanel : MonoBehaviour
{
    private SpaceShip currentShip;
    public static ShipInfoPanel instance;
    //public Text activeCharacterLabel;

    public void Awake()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        currentShip = null;
        gameObject.SetActive(false);
    }

    public SpaceShip CurrentShip
    {
        get
        {
            return currentShip;
        }

        set
        {
            SpaceShip newActive = value;
            if (newActive != currentShip)
            {
                currentShip = value;
                updateCurrentShip();
            }
        }
    }

    public void updateCurrentShip()
    {
        //activeCharacterLabel.text = activeCharacter.Name + "'s Stats";
        foreach (KeyValuePair<string, int> stat in currentShip.ShipResources)
        {
            //Debug.Log("stat: " + stat + "activeCharacter: " + ship.Name + "key: " + stat.Key + "value: " + stat.Value);
            ShipStatPanel.instance.setStat(stat.Key, stat.Value.ToString());
        }

    }
}
