using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GameData : MonoBehaviour {

    public static GameData instance;
    public Character player1;
    public List<Character> party = new List<Character>();
    public static int characterIndex;
    public Sprite defaultEventSprite = null;
    public Dictionary<string, CharacterTrait> traitDictionary;
    public Dictionary<string, Sprite> eventSpriteDictionary;
    public Dictionary<string, Sprite> characterPortraitDictionary;
    public List<string> races, genders;
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
        createRaceList();
        createGenderList();
        createTraitDictionary();
    }

    public Sprite getEventSprite(string eventName)
    {
        if (eventSpriteDictionary.ContainsKey(eventName))
        {
            return eventSpriteDictionary[eventName];
        } else
        {
            return defaultEventSprite;
        }
    }

    void LoadSprites()
    {
        eventSpriteDictionary = new Dictionary<string, Sprite>();
        Sprite[] eventSprites = Resources.LoadAll<Sprite>("Images/EventImages/");

        foreach (Sprite s in eventSprites)
        {
            //Debug.Log(s.name);
            eventSpriteDictionary[s.name] = s;
        }

        characterPortraitDictionary = new Dictionary<string, Sprite>();
        Sprite[] portraitSprites = Resources.LoadAll<Sprite>("Images/CharacterPortraits/");

        foreach (Sprite s in portraitSprites)
        {
            Debug.Log(s.name);
            characterPortraitDictionary[s.name] = s;
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

   public void createRaceList()
    {
        races = new List<string>();
        races.Add("Terrestrial");
        races.Add("Martian");
        races.Add("Xorpan");

    }

    public string getRandomRace()
    {
        return races[Random.Range(0, races.Count)];
    }

    public void createGenderList()
    {
        genders = new List<string>();
        genders.Add("Male");
        genders.Add("Female");
        genders.Add("Neutral");
    }

    public string getRandomGender()
    {
        return genders[Random.Range(0, genders.Count)];
    }

}
