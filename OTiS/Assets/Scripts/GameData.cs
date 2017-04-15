using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public static GameData instance;
    public Character player1;
    public static int characterIndex;
    public Dictionary<string, CharacterTrait> traitDictionary;
    // Use this for initialization
    void Awake () {
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

        createTraitDictionary();
    }


    public int nextCharID()
    {
        return characterIndex += 1;
    }

    public void createTraitDictionary()
    {
        traitDictionary = new Dictionary<string, CharacterTrait>();
        traitDictionary.Add("Engineer", new CharacterTrait("Engineer", "Enginners are adept at mechanical workings."));
        traitDictionary.Add("Diplomat", new CharacterTrait("Diplomat", "A diplomat has exeptional negotiating skills."));

    }


}
