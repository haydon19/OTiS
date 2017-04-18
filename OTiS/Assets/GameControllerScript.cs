using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Start, Active, GameOver};

public enum Stat { Strength, Smarts, Agility };

public enum Resource { Fuel, Shields, Medicine };

public enum LocationState { Space, Land };

public class GameControllerScript : MonoBehaviour {
    public PartyManager party;
    public float eventRate = 2.0f;
    public float nextEvent = 0.0f;
    public List<Character> Enemies = new List<Character>();
    public bool choosing = false;
    public static GameControllerScript instance;
    public Dictionary<int, string> names;
    List<string> shipNames;
    public List<string> characterStatNames = new List<string> { "Strength", "Agility", "Smarts" };
    public List<string> resourceNames = new List<string> { "Fuel", "Shields", "Ammo" };

    public Dictionary<OptionType, List<string>> options;
    public Dictionary<EventType, Event> possibleEvents;
    public Dictionary<EventType, List<string>> Event;
    public Dictionary<EventType, List<OptionType>> eventOptions;
    public Dictionary<OptionType, List<string>> optionStrings;
    public Dictionary<string, Enemy> enemyDictionary;
    List<string> enemyKeyList;
    int characterIndex = 0;
    GameState gameState;
    public SpaceShip ship;
    public bool paused = false;
    public List<EventType> validEvents;
    public SpaceShip enemyShip;
    public LocationState locationState = LocationState.Space;

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

    public void createShipNameList()
    {
        shipNames = new List<string>();
        shipNames.Add("Neverwhatever");
        shipNames.Add("Star Destroyer");
        shipNames.Add("Her Majesty");
        shipNames.Add("Qtaz'Tzor");
        shipNames.Add("The Justice");
        shipNames.Add("Wanderer");
        shipNames.Add("Cheese Mechana");


    }

    public void createEventOptionsDictionary()
    {
        eventOptions = new Dictionary<EventType, List<OptionType>>();
        eventOptions.Add(EventType.RogueAsteroid, new List<OptionType> { OptionType.Avoid, OptionType.Blast, OptionType.Land });
        eventOptions.Add(EventType.EnemyShip, new List<OptionType> { OptionType.Avoid, OptionType.Blast, OptionType.Board });
        eventOptions.Add(EventType.EncounterNPC, new List<OptionType> { OptionType.Recruit, OptionType.Gossip, OptionType.Intimidate });
        eventOptions.Add(EventType.EncounterPlanet, new List<OptionType> { OptionType.Land, OptionType.Scan, OptionType.Bypass });
        //options.Add(OptionType.Blast, " ")

    }

    public void createPossibleEventsDictionary()
    {
        possibleEvents = new Dictionary<EventType, Event>();
        possibleEvents.Add(EventType.RogueAsteroid, new RogueAsteroidEvent(EventType.RogueAsteroid, 0));
        possibleEvents.Add(EventType.EnemyShip, new EnemyShipEvent(EventType.EnemyShip, 1));
        possibleEvents.Add(EventType.EncounterNPC, new EncounterNPC(EventType.EncounterNPC, 2));
        possibleEvents.Add(EventType.Statement, new StatementEvent(EventType.Statement, 3));
        possibleEvents.Add(EventType.GameOver, new GameOverEvent(EventType.Statement, 4));
        possibleEvents.Add(EventType.EncounterPlanet, new EncounterPlanet(EventType.EncounterPlanet, 5));


        //options.Add(OptionType.Blast, " ")

    }

    public void createOptionStringsDictionary()
    {
        optionStrings = new Dictionary<OptionType, List<string>>();
        optionStrings.Add(OptionType.Blast, new List<string> { "<actor> attempts to blast <subject> with it's lazers", "<actor> runs for the guns to shoot at <subject>" });
        optionStrings.Add(OptionType.Avoid, new List<string> { "<actor> dips and dives to avoid <subject>", "<actor> uses ace pilot skills to manuever around <subject>" });
        optionStrings.Add(OptionType.Land, new List<string> { "<actor> brings the ship down to <subject>", "<actor> lowers the landing gear in attempts to land on <subject>" });
        optionStrings.Add(OptionType.Board, new List<string> { "<actor> rams into <subject>", "<actor> tries to get close enough to board <subject>" });
        optionStrings.Add(OptionType.Gossip, new List<string> { "<actor> listens to <subject> talk about his adventures", "<actor> chats about the local area with <subject>" });
        optionStrings.Add(OptionType.Intimidate, new List<string> { "<actor> attempts to scare <subject> with a loud roar", "<actor> brandishes a gun to <subject> to show them they means business" });
        optionStrings.Add(OptionType.Recruit, new List<string> { "<actor> trys to recruit <subject> by offering pizza flavoured chips", "<actor> eyes over <subject>'s resume then offers them a job" });
        optionStrings.Add(OptionType.Scan, new List<string> { "<actor> scans <subject> for lifeforms and resources", "<actor> scans <subject> for lifeforms and resources" });
        optionStrings.Add(OptionType.Bypass, new List<string> { "<actor> decides it's not worth the crews time and flies the <partyship> past <subject>", "There's no time, we must fly around <subject>" });


        //options.Add(OptionType.Blast, " ")

    }



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

    public Enemy getRandomEnemy()
    {
        int rand = Random.Range(0, enemyKeyList.Count);
        string randomKey = enemyKeyList[rand];
        enemyDictionary[randomKey].ID += 1;
        Enemy enemy = new Enemy(enemyDictionary[randomKey].Name, enemyDictionary[randomKey].ID, enemyDictionary[randomKey].getStat("Strength"), enemyDictionary[randomKey].getStat("Smarts"), enemyDictionary[randomKey].getStat("Agility"), enemyDictionary[randomKey].getStat("Piloting"));
        return enemy;
    }

    public Character createNPC()
    {
        return new Character(getRandomName(), GameData.instance.nextCharID(), Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
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

        validEvents = new List<EventType>();
        PartyManagerPanel.instance.gameObject.SetActive(false);
        //Create prototype lists, eventually this will just be importing from an XML file or something
        createNameDictionary();
        createEnemyPrototypes();
        createEventOptionsDictionary();
        createPossibleEventsDictionary();
        createOptionStringsDictionary();
        createShipNameList();
        characterIndex = 0;

        party = new PartyManager();
        //Debug.Log(GameControllerScript.instance.party.ToString());
        if(GameData.instance.party != null)
        {
            foreach(Character c in GameData.instance.party)
            party.addPartyMember(c);
        }

        //Lets create some Characters, we'll randomize their starting stats based on 2d6
        while(party.partyMembers.Count < 4)
        {
            party.addPartyMember();
        }

        CharacterIconsPanel.instance.populateContainer();
    }



    //function for quitting the game
    public void ExitGame() {
        //use this later when inputs are set up
       // if (Input.GetKeyDown(KeyCode.Escape)) { };
        
            Application.Quit();
        Debug.Log("Game is exiting");
    }

    public EventType getRandomEvent()
    {
        validEvents.Clear();

            validEvents.Add(EventType.Statement);
        
        /*
        if (Enemies.Count < 5)
        {
            validEvents.Add(EventType.NewEnemy);
        }*/

        validEvents.Add(EventType.RogueAsteroid);
        validEvents.Add(EventType.EnemyShip);
        validEvents.Add(EventType.EncounterNPC);
        validEvents.Add(EventType.EncounterPlanet);
        /*
        if(enemyShip == null)
        {
            validEvents.Add(EventType.EnemyShip);
        }
        */

        /*
        if(ship != null && ship.getStat("Shields") > 0 && enemyShip != null && enemyShip.getStat("Shields") > 0)
        {
            validEvents.Add(EventType.SpaceCombat);
        }
        */
        return validEvents[Random.Range(0, validEvents.Count)];

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
            return;
        }
        //This is the timer for the events, also we don't have a new event if we are currently making a decision
        if (Time.time > nextEvent && !choosing && !paused)
        {
            nextEvent = Time.time + 0.5f;

                    possibleEvents[getRandomEvent()].EnterEvent();

            if (party.getParty().Count == 0)
            {
                gameState = GameState.GameOver;
                new GameOverEvent(EventType.GameOver,  EventLog.instance.transform.childCount);
                //EventLog.instance.newLogItem();
            }

        }
        
	}
}
