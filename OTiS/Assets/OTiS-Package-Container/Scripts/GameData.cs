using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//This class holds all kind of data about the game
//There are a bunch of functions and data creation that needs to be moved here
public class GameData : MonoBehaviour {

    public static GameData instance;
    public Character player1;
    public List<Character> party = new List<Character>();
    public static int characterIndex;
    public Sprite defaultEventSprite = null;
    public Dictionary<string, Trait> traitDictionary;
    public Dictionary<string, Sprite> eventSpriteDictionary;
    public Dictionary<string, Sprite> characterPortraitDictionary;
    public Dictionary<string, Module> modulePrototypes;
    public List<Situation> situationPrototypes;
    public Situation shipBattle;

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
        createModulePrototypes();
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

    public void createModulePrototypes()
    {
        modulePrototypes = new Dictionary<string, Module>();
        modulePrototypes.Add("Laser Gun", new WeaponModule(ShipWeaponType.Laser, new Attack(1, 10), null));
    }

    public void createTraitDictionary()
    {
        traitDictionary = new Dictionary<string, Trait>();
        traitDictionary.Add("Engineer", new Trait("Engineer", "Enginners are adept at mechanical workings.", new List<StatBonus> { new StatBonus("Strength", 2), new StatBonus("Piloting", 1)}));
        traitDictionary.Add("Diplomat", new Trait("Diplomat", "A diplomat has exeptional negotiating skills.", new List<StatBonus> { new StatBonus("Mind", 2), new StatBonus("Strength", 1) }));
        traitDictionary.Add("Pirate", new Trait("Pirate", "Pirates will do whatever it takes to make some coin, even questionable things.", new List<StatBonus> { new StatBonus("Piloting", 2), new StatBonus("Agility", 1) }));
        traitDictionary.Add("Botanist", new Trait("Botanist", "A botanist can identify strange plants and even learn to grow their own.", new List<StatBonus> { new StatBonus("Mind", 3) }));
        traitDictionary.Add("Sharpshooter", new Trait("Sharpshooter", "When it comes to gunning, a sharpshooter never misses.", new List<StatBonus> { new StatBonus("Agility", 2), new StatBonus("Mind", 1) }));
        traitDictionary.Add("Explorer", new Trait("Explorer", "Explorers yearn for adventure and always seem to happen upon mysterious locales.", new List<StatBonus> { new StatBonus("Agility", 1), new StatBonus("Mind", 1), new StatBonus("Strength", 1), new StatBonus("Piloting", 1) }));

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
