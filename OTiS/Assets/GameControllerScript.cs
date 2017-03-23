using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Start, Active, GameOver};

public class GameControllerScript : MonoBehaviour {
    public CharacterInfoPanel characterInfoPanel;
    public float eventRate = 2.0f;
    public float nextEvent = 0.0f;
    public List<Character> Party = new List<Character>();
    public List<Character> Enemies = new List<Character>();
    public bool choosing = false;
    public static GameControllerScript instance;
    public Dictionary<int, string> names;
    public Dictionary<string, Enemy> enemyDictionary;
    List<string> enemyKeyList;
    int characterIndex = 0;
    GameState gameState;
    public List<EventType> validEvents;

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

    public void createEnemyPrototypes()
    {
        enemyDictionary = new Dictionary<string, Enemy>();
        enemyDictionary.Add("Zombie", new Enemy("Zombie", 0, 8, 1, 2));
        enemyDictionary.Add("Feral Cat", new Enemy("Feral Cat", 0, 3, 4, 6));
        enemyDictionary.Add("Robot", new Enemy("Robot", 0, 6, 6, 2));
        enemyDictionary.Add("Alien", new Enemy("Alien", 0, 2, 10, 5));
        enemyDictionary.Add("Horse", new Enemy("Horse", 0, 5, 2, 6));

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
        characterIndex = 0;

        

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
            validEvents.Add(EventType.Greeting);
        }

        if (Enemies.Count > 0)
        {
            validEvents.Add(EventType.Combat);
        }

        if (Enemies.Count < 5)
        {
            validEvents.Add(EventType.NewEnemy);
        }

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
            Character c;
            nextEvent = Time.time + eventRate;
            Event newEvent;
            //int eventNum = Random.Range(0, 3);
            //I have 3 different events right now, so this randomly chooses 1
            switch (getRandomEvent()) { 
                case EventType.Greeting:
                    //This event makes 2 random party members greet eachother
                    newEvent = new Event(EventType.Greeting, new List<Character> { Party[Random.Range(0, Party.Count)], Party[Random.Range(0, Party.Count)] }, EventLog.instance.transform.childCount);

                   // EventLog.instance.newLogItem(newEvent);

                    break;
                case EventType.Combat:
                    if (Enemies.Count < 1)
                        return;
                    Character partyMember = Party[Random.Range(0, Party.Count)];
                    Character enemy = Enemies[Random.Range(0, Enemies.Count)];
                    //This event makes a random party member and enemy attack one another
                    newEvent = new Event(EventType.Combat, new List<Character> { enemy, partyMember }, EventLog.instance.transform.childCount);
                   // EventLog.instance.newLogItem(newEvent);
                    break;
                
                case EventType.NewEnemy:
                    c = getRandomEnemy();
                    Enemies.Add(c);
                    EnemyInfoPanel.instance.newCharacter(c);
                    newEvent = new Event(EventType.NewEnemy, new List<Character> { c, Party[Random.Range(0, Party.Count)] }, EventLog.instance.transform.childCount);
                    //characterInfoPanel.newCharacter(temp);
                    //EventLog.instance.newLogItem(newEvent);



                    break;
                /*
                case 3:
                    //This is the event that halts the event system and waits for the player to choose an option
                    c = Party[Random.Range(0, Party.Count)];
                    //Create the event
                    newEvent = new Event(EventType.Decision, new List<Character> { c }, EventLog.instance.transform.childCount);

                    OptionMenuController.instance.addOptionItems(newEvent);
                    EventLog.instance.newLogItem(newEvent);
                    choosing = true;
                    break;
                    */
            }

            if (Party.Count == 0)
            {
                gameState = GameState.GameOver;
                new Event(EventType.GameOver, new List<Character> { }, EventLog.instance.transform.childCount);
                //EventLog.instance.newLogItem();
            }

        }
        
	}
}
