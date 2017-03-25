using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Start, Active, GameOver};

public enum LocationState { Space, Land };

public class GameControllerScript : MonoBehaviour {
    public CharacterInfoPanel characterInfoPanel;
    public float eventRate = 2.0f;
    public float nextEvent = 0.0f;
    public List<Character> Party = new List<Character>();
    public List<Character> Enemies = new List<Character>();
    public bool choosing = false;
    public static GameControllerScript instance;
    public Dictionary<int, string> names;
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

    public void createEventOptionsDictionary()
    {
        eventOptions = new Dictionary<EventType, List<OptionType>>();
        eventOptions.Add(EventType.RogueAstroid, new List<OptionType> { OptionType.Avoid, OptionType.Blast, OptionType.Land });
        eventOptions.Add(EventType.EnemyShip, new List<OptionType> { OptionType.Avoid, OptionType.Blast, OptionType.Board });
        //options.Add(OptionType.Blast, " ")

    }

    public void createPossibleEventsDictionary()
    {
        possibleEvents = new Dictionary<EventType, Event>();
        possibleEvents.Add(EventType.RogueAstroid, new RogueAstroidEvent(EventType.RogueAstroid, 0));
        possibleEvents.Add(EventType.EnemyShip, new EnemyShipEvent(EventType.EnemyShip, 1));

        possibleEvents.Add(EventType.Statement, new StatementEvent(EventType.Statement, 2));
        possibleEvents.Add(EventType.GameOver, new GameOverEvent(EventType.Statement, 3));


        //options.Add(OptionType.Blast, " ")

    }

    public void createOptionStringsDictionary()
    {
        optionStrings = new Dictionary<OptionType, List<string>>();
        optionStrings.Add(OptionType.Blast, new List<string> { " attempts to blast the ", " runs for the guns to shoot at " });
        optionStrings.Add(OptionType.Avoid, new List<string> { " dips and dives to avoid the ", " uses ace pilot skills to manuever around the " });
        optionStrings.Add(OptionType.Land, new List<string> { " brings the ship down to ", " lowers the landing gear in attempts to land on the " });
        optionStrings.Add(OptionType.Board, new List<string> { " rams into the ", " tries to get close enough to board " });
        //options.Add(OptionType.Blast, " ")

    }



    public void createEnemyPrototypes()
    {
        enemyDictionary = new Dictionary<string, Enemy>();
        enemyDictionary.Add("Zombie", new Enemy("Zombie", 0, 8, 1, 2));
        enemyDictionary.Add("Feral Cat", new Enemy("Feral Cat", 0, 3, 4, 10));
        enemyDictionary.Add("Robot", new Enemy("Robot", 0, 6, 6, 2));
        enemyDictionary.Add("Alien", new Enemy("Alien", 0, 2, 10, 6));
        enemyDictionary.Add("Horse", new Enemy("Horse", 0, 5, 2, 7));

        enemyKeyList = new List<string>(enemyDictionary.Keys);
    }

    public Enemy getRandomEnemy()
    {
        int rand = Random.Range(0, enemyKeyList.Count);
        string randomKey = enemyKeyList[rand];
        enemyDictionary[randomKey].ID += 1;
        Enemy enemy = new Enemy(enemyDictionary[randomKey].Name, enemyDictionary[randomKey].ID, enemyDictionary[randomKey].getStat("Strength"), enemyDictionary[randomKey].getStat("Smarts"), enemyDictionary[randomKey].getStat("Agility"));
        return enemy;
    }

    public Character getRandomPartyMember()
    {
        return Party[Random.Range(0, Party.Count)];
    }

    public string getRandomName()
    {
        int r = Random.Range(0,8);
        return names[r];
    }

    int nextCharID()
    {
        return characterIndex += 1;
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
        //Create prototype lists, eventually this will just be importing from an XML file or something
        createNameDictionary();
        createEnemyPrototypes();
        createEventOptionsDictionary();
        createPossibleEventsDictionary();
        createOptionStringsDictionary();
        characterIndex = 0;


        ship = new SpaceShip("The Aloha", 10);
        //Lets create some Characters, we'll randomize their starting stats based on 2d6
        Character temp;
        temp = new Character(getRandomName(), nextCharID(), Random.Range(1, 7) + Random.Range(1, 7), Random.Range(1, 7) + Random.Range(1, 7), Random.Range(1, 7) + Random.Range(1, 7));
        Party.Add(temp);
        characterInfoPanel.newCharacter(temp);
        temp = new Character(getRandomName(), nextCharID(), Random.Range(1, 7) + Random.Range(1, 7), Random.Range(1, 7) + Random.Range(1, 7), Random.Range(1, 7) + Random.Range(1, 7));
        Party.Add(temp);
        characterInfoPanel.newCharacter(temp);
        temp = new Character(getRandomName(), nextCharID(), Random.Range(1, 7) + Random.Range(1, 7), Random.Range(1, 7) + Random.Range(1, 7), Random.Range(1, 7) + Random.Range(1, 7));
        Party.Add(temp);
        characterInfoPanel.newCharacter(temp);
        temp = new Character(getRandomName(), nextCharID(), Random.Range(1, 7) + Random.Range(1, 7), Random.Range(1, 7) + Random.Range(1, 7), Random.Range(1, 7) + Random.Range(1, 7));
        Party.Add(temp);
        characterInfoPanel.newCharacter(temp);

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

        if(Party.Count > 1)
        {
            validEvents.Add(EventType.Statement);
        }
        /*
        if (Enemies.Count < 5)
        {
            validEvents.Add(EventType.NewEnemy);
        }*/

        validEvents.Add(EventType.RogueAstroid);
        validEvents.Add(EventType.EnemyShip);
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

// Update is called once per frame
void Update () {

      if(gameState == GameState.GameOver)
        {
            return;
        }
        //This is the timer for the events, also we don't have a new event if we are currently making a decision
        if (Time.time > nextEvent && !choosing)
        {
            nextEvent = Time.time + eventRate;

                    possibleEvents[getRandomEvent()].EnterEvent();

            if (Party.Count == 0)
            {
                gameState = GameState.GameOver;
                new GameOverEvent(EventType.GameOver,  EventLog.instance.transform.childCount);
                //EventLog.instance.newLogItem();
            }

        }
        
	}
}
