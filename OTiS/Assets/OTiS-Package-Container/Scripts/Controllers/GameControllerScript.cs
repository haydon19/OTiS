using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Start, Active, GameOver};

public enum Stat { Strength, Mind, Agility };

public enum Resource { Fuel, Shields, Medicine };

public enum LocationState { Space, Land };

public class GameControllerScript : MonoBehaviour {
    int lightYearsToEOU;
    public PartyManager party;
    public float eventRate = 2.0f;
    public float nextEvent = 0.0f;
    public List<Character> Enemies = new List<Character>();
    public bool choosing = false;
    public static GameControllerScript instance;
    public Dictionary<int, string> names;
    List<string> shipNames;
    public List<string> characterStatNames = new List<string> { "Strength", "Agility", "Mind" };
    public List<string> resourceNames = new List<string> { "Fuel", "Ammo" };

    public Dictionary<string, Enemy> enemyDictionary;
    List<string> enemyKeyList;
    int characterIndex = 0;
    GameState gameState = GameState.Active;
    public SpaceShip ship;
    public bool paused = false;
    public List<SituationTag> validEvents;
    public SpaceShip enemyShip;
    [SerializeField]
    public LocationState locationState = LocationState.Space;

    public Situation currentSituation;

    public int LightYearsToEOU
    {
        get
        {
            return lightYearsToEOU;
        }

        set
        {
            int oldValue = lightYearsToEOU;
            if (value != oldValue)
            {
                lightYearsToEOU = value;
                EventPanelScript.instance.LightYearCounter.text = lightYearsToEOU + " Light Years to go";
            }

        }
    }
    //Dictionary of character names, for generating new characters

    public void createNameDictionary()
    {
        names = new Dictionary<int, string>();
        names.Add(0, "Steve");
        names.Add(1, "Jerry");
        names.Add(2, "Margret");
        names.Add(3, "Jessica");
        names.Add(4, "Trisha");
        names.Add(5, "Bull");
        names.Add(6, "Nathan");
        names.Add(7, "Clarissa");

    }

    //Dictionary of ship names, for generating new ships

    public void createShipNameList()
    {
        shipNames = new List<string>();
        shipNames.Add("The Neverwhatever");
        shipNames.Add("The Star Destroyer");
        shipNames.Add("Her Majesty's Vessel");
        shipNames.Add("The Qtaz'Tzor");
        shipNames.Add("The Justice");
        shipNames.Add("The Wanderer");
        shipNames.Add("The Cheese Mechana");

    }

    //Prototypes of different enemies

    public void createEnemyPrototypes()
    {
        enemyDictionary = new Dictionary<string, Enemy>();
        enemyDictionary.Add("Zombie", new Enemy("Zombie", 0, 8, 1, 2, 0));
        enemyDictionary.Add("Feral Cat", new Enemy("Feral Cat", 0, 3, 4, 10, 0));
        enemyDictionary.Add("Robot", new Enemy("Robot", 0, 6, 6, 2, 0));
        enemyDictionary.Add("Alien", new Enemy("Alien", 0, 2, 10, 6, 0));
        enemyDictionary.Add("Horse", new Enemy("Horse", 0, 5, 2, 7, 0));

        enemyKeyList = new List<string>(enemyDictionary.Keys);
    }

    //When we want a new enemy, we get a random one using this function
    public Enemy getRandomEnemy()
    {
        int rand = Random.Range(0, enemyKeyList.Count);
        string randomKey = enemyKeyList[rand];
        enemyDictionary[randomKey].ID += 1;
        Enemy enemy = new Enemy(enemyDictionary[randomKey].Name, enemyDictionary[randomKey].ID, enemyDictionary[randomKey].getStat("Strength"), enemyDictionary[randomKey].getStat("Mind"), enemyDictionary[randomKey].getStat("Agility"), enemyDictionary[randomKey].getStat("Piloting"));
        return enemy;
    }

    //Creating non player characters, giving them random stats, races, genders, etc.
    public Character createNPC()
    {
        return new Character(getRandomName(), GameData.instance.getRandomRace(), GameData.instance.getRandomGender(), GameData.instance.nextCharID(), Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
    }

    public Character getRandomPartyMember()
    {
        return party.getParty()[Random.Range(0, party.getParty().Count)];
    }

    public string getRandomName()
    {
        int r = Random.Range(0,8);
        return names[r];
    }

    public string getRandomShipName()
    {
        
        return shipNames[Random.Range(0,shipNames.Count)];
    }

    
    // Use this for initialization
    void Start () {

        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

        validEvents = new List<SituationTag>();
        PartyManagerPanel.instance.gameObject.SetActive(false);

        //Create prototype lists, eventually this will just be importing from an XML file or something
        createNameDictionary();
        createEnemyPrototypes();
        createShipNameList();
        characterIndex = 0;
        LightYearsToEOU = 30;

        party = new PartyManager();
        //Debug.Log(GameControllerScript.instance.party.ToString());
        if(GameData.instance.party != null)
        {
            foreach(Character c in GameData.instance.party)
            party.addPartyMember(c);
        }



        //Lets create some Characters, we'll randomize their starting stats (done in the character creation function)
        while (party.partyMembers.Count < 4)
        {
            party.addPartyMember();
        }

        CharacterIconsPanel.instance.populateContainer();
    }


    
    //function for quitting the game
    public void ExitGame() {
        //use this later when inputs are set up
        //Dont use this if on iphone
       // if (Input.GetKeyDown(KeyCode.Escape)) { };
        
            Application.Quit();
        Debug.Log("Game is exiting");
    }

    public SituationTag getRandomEvent()
    {
        validEvents.Clear();
        if (enemyShip != null) {
            validEvents.Add(SituationTag.ShipCombat);
            return validEvents[Random.Range(0, validEvents.Count)];

        }
        switch (locationState)
        {
            case LocationState.Space:
                validEvents.Add(SituationTag.EncounterAsteroid);
                validEvents.Add(SituationTag.EncounterShip);
                validEvents.Add(SituationTag.EncounterPlanet);
                break;

            case LocationState.Land:
                validEvents.Add(SituationTag.EncounterNPC);
                break;

        }

        validEvents.Add(SituationTag.Statement);



        return validEvents[Random.Range(0, validEvents.Count)];

    }

    public Situation GetRandomSituation()
    {
        return Instantiate(GameData.instance.situationPrototypes[Random.Range(0, GameData.instance.situationPrototypes.Count)]);
    }

    void GetInput()
    {
        if (Input.GetKeyUp("i"))
        {
            // PartyManagerPanel.instance.gameObject.SetActive(!PartyManagerPanel.instance.gameObject.activeSelf);
            PartyManagerPanel.instance.openPartyManagerPanel();
        }
    }

    public void pauseGame()
    {
        paused = !paused;
    }

// Update is called once per frame
void Update () {


        GetInput();
        if(SoundControllerScript.instance != null)
        {
            //SoundControllerScript.instance.PlayMusic(1);
        }
        if(gameState == GameState.GameOver)
        {
            //if the game is over stop doing the game loop
            return;
        }
      
        if(currentSituation == null)
        {
            currentSituation = GetRandomSituation();
            currentSituation.Initialize();
        }
        
	}
}
