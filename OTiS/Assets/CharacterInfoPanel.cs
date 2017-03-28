using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CharacterInfoPanel : MonoBehaviour {
    public CharacterContainer characterTabs;

    private Character activeCharacter;
    public static CharacterInfoPanel instance;
    public Text activeCharacterLabel;

    public void Awake()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        activeCharacter = null;
        gameObject.SetActive(false);

    }

    public Character ActiveCharacter
    {
        get
        {
            return activeCharacter;
        }

        set
        {
            Character newActive = value;
            if (newActive != activeCharacter)
            {
                activeCharacter = value;
                updateActiveCharacter();
            }
        }
    }

    public void updateActiveCharacter()
    {
        activeCharacterLabel.text = activeCharacter.Name + "'s Stats";
        foreach (KeyValuePair<string, int> stat in activeCharacter.Stats)
        {
            Debug.Log("stat: " + stat + "activeCharacter: " + activeCharacter.Name + "key: " + stat.Key + "value: " + stat.Value);
            CharacterStatPanel.instance.setStat(stat.Key, stat.Value.ToString());
        }

    }
}
