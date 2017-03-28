using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CharacterInfoPanel : MonoBehaviour {
    public CharacterContainer characterTabs;
    public CharacterStatPanel characterStats;

    private Character activeCharacter;
    public static CharacterInfoPanel instance;

    public void Start()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        activeCharacter = null;
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
        foreach (KeyValuePair<string, int> stat in activeCharacter.Stats)
        {
            characterStats.setStat(stat.Key, stat.Value.ToString());
        }

    }
}
