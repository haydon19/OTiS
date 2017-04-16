using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GameData : MonoBehaviour {

    public static GameData instance;
    public Character player1;
    public static int characterIndex;
    public Dictionary<string, CharacterTrait> traitDictionary;
    public Dictionary<string, Sprite> eventSpriteDictionary;
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

        LoadSprites();
        createTraitDictionary();
    }

    void LoadSprites()
    {
        eventSpriteDictionary = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/EventImages/");

        Debug.Log("LOADED RESOURCE:");
        foreach (Sprite s in sprites)
        {
            Debug.Log(s.name);
            eventSpriteDictionary[s.name] = s;
        }
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
        traitDictionary.Add("Pirate", new CharacterTrait("Pirate", "Pirates will do whatever it takes to make some coin, even questionable things."));
        traitDictionary.Add("Botanist", new CharacterTrait("Botanist", "A botanist can identify strange plants and even learn to grow their own."));
        traitDictionary.Add("Sharpshooter", new CharacterTrait("Sharpshooter", "When it comes to gunning, a sharpshooter never misses."));
        traitDictionary.Add("Explorer", new CharacterTrait("Explorer", "Explorers yearn for adventure and always seem to happen upon mysterious locales."));

    }


}
