using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { Statement, Death, GameOver, EnemyShip, SpaceCombat, RogueAsteroid, EncounterNPC, EncounterPlanet, EmptyShip, ShipCombat };

public abstract class Event {

    
    protected string summary;
    protected string logEntry;

    int id;
    List<Option> options;
    protected string subject;
    EventType type;
    public List<Character> characters;
    public SpaceShip ship;

    public bool randomSkillGain(Character character,string stat)
    {
        if(Random.Range(0, 7) > 4)
        {
            character.changeStat(stat, 1);
            return true;
        }
        return false;
    }

    public bool randomSkillLoss(Character character, string stat)
    {
        if (Random.Range(0, 7) > 4)
        {
            character.changeStat(stat, -1);
            return true;
        }
        return false;
    }

    

    public string Summary
    {
        get
        {
            return summary;
        }

        set
        {
            summary = value;
        }
    }


    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public List<Option> Options
    {
        get
        {
            return options;
        }

        set
        {
            options = value;
        }
    }

    public EventType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public Event(EventType type, int ID)
    {
        characters = new List<Character>();
        id = ID;
        this.type = type;

    }

    public void getCharacters(int num)
    {
        for (int i = 0; i < num; i++)
        {
            characters.Add(GameControllerScript.instance.getRandomPartyMember());
        }
    }


    public void LogEventSummary()
    {
        EventLog.instance.newLogItem(summary);
    }

    public void LogEntry(string entry)
    {
        EventLog.instance.newLogItem(entry);
    }



    public virtual void setSummary()
    {
        summary = "Default Summary";
    }

    public virtual void EnterEvent()
    {

        characters.Clear();

        getOptions();
        setSummary();

        LogEventSummary();

    }

    public virtual void HandleEvent(OptionType oType)
    {

    }

    public virtual void getOptions()
    {
        Character c;
        Options = new List<Option>();
        foreach (OptionType o in GameControllerScript.instance.eventOptions[Type])
        {
            c = GameControllerScript.instance.getRandomPartyMember();
            characters.Add(c);
            Options.Add(new Option(this, o, c, subject));
        }
        OptionMenuController.instance.addOptionItems(this);
        GameControllerScript.instance.choosing = true;
    }
}



public class StatementEvent : Event
{

    public StatementEvent(EventType type, int id) : base(type, id)
    {

    }

    public override void EnterEvent()
    {
        setSummary();
        LogEventSummary();
    }

    public override void setSummary()
    {
        summary = GameControllerScript.instance.getRandomPartyMember().Name + " makes a statement.";

    }

}

public class GameOverEvent : Event
{

    public GameOverEvent(EventType type, int id) : base(type, id)
    {
        
    }

    public override void EnterEvent()
    {
        setSummary();
        LogEventSummary();
    }

    public override void setSummary()
    {
        LogEntry(" The party has died. Game Over.");

    }

}

public class EmptyShipEvent : Event
{

    public EmptyShipEvent(EventType type, int id) : base(type, id)
    {

    }

    public override void EnterEvent()
    {
        setSummary();
        LogEventSummary();
    }

    public override void setSummary()
    {
        LogEntry(" The ship has run out of fuel. The party slowly starves to death drifting in space.");


        
        foreach (Character c in GameControllerScript.instance.party.partyMembers)
        {
            c.Damage(c.getStat("Health"));
        }

        GameControllerScript.instance.party.partyMembers.RemoveAll(item => item.Dead);
    }

}

public class RogueAsteroidEvent : Event
{

    public RogueAsteroidEvent(EventType type, int id) : base(type, id)
    {

    }

    public override void EnterEvent()
    {
        characters.Clear();

        subject = "the rogue asteroid";
        ship = GameControllerScript.instance.party.ship;
        getOptions();
        setSummary();

        EventPanelScript.instance.SetEvent(this);
    }

    public override void HandleEvent(OptionType oType)
    {
        switch (oType)
        {
            case (OptionType.Avoid):
                if (characters[0].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    if (randomSkillGain(characters[0], "Piloting"))
                    {

                        LogEntry("You seem to be at one with the controls of the ship, you feel your skills as a pilot grow!\n\n" + characters[0].Name + "'s Piloting increases! " + characters[0].getStat("Piloting"));


                    }
                    else {
                        LogEntry("You race to the helm, and dive into the pilots seat in an effort to steer the ship clear of the asteroid. A narrow success.");
                        GameControllerScript.instance.party.ship.Damage(5);
                        //LogEntry(ship.Name + " has lost " + 5 + " shields.");
                        //GameControllerScript.instance.party.changeShipStat("Shields", -5);
                    }
                }
                else {

                    if (randomSkillLoss(characters[0], "Piloting"))
                    {
                        characters[0].changeStat("Piloting", -1);
                        LogEntry("You scrape against the asteriods hard exterior, a compartment of the ship holding ESSENTIAL supplies has been damaged and it's contents blown into space! Maybe you weren't meant to hang around the pilots seat...\n\n" + characters[0].Name + "'s Piloting decreases!  " + characters[0].getStat("Piloting"));
                    }
                    else {
                        LogEntry("Your heroic dive to the pilots seat is misaligned, luckily it wasn't the hardest asteroid in the galaxy, and your ship destroys it with it's hull.");
                    }
                }
                GameControllerScript.instance.LightYearsToEOU -= 2;
                EventActions.loseResource(this, "Fuel");
                break;
            case (OptionType.Blast):
                if (characters[1].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry("Ya fuckin blasted it bud.");
                }
                else
                {
                    LogEntry("Ya fucked up blasting it.");
                }
                GameControllerScript.instance.LightYearsToEOU -= 1;
                EventActions.loseResource(this, "Fuel");
                break;
            case (OptionType.Land):
                if (characters[2].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry(characters[2].Name + " has successfully landed on the Asteroid, allowing the party to scavange for resources.");
                    EventActions.gainRandomResource(this);
                    EventActions.gainRandomResource(this);
                    GameControllerScript.instance.locationState = LocationState.Land;
                }
                else
                {
                    LogEntry(characters[2].Name + " misjudges the landing and the ship scapes along the surface of the asteroid. The party decides to cut their losses and not try again.");
                    GameControllerScript.instance.party.ship.Damage(5);
                    GameControllerScript.instance.LightYearsToEOU -= 1;
                    EventActions.loseResource(this, "Fuel");
                }

                break;
        }
    }

    public override void setSummary()
    {
        summary = "You are travelling through space... A rogue asteroid appears!";
    }

}

public class EnemyShipEvent : Event
{

    public EnemyShipEvent(EventType type, int id) : base(type, id)
    {

    }

    SpaceShip enemyShip;
    public override void EnterEvent()
    {

        characters.Clear();
        //This should be the enemy ship
        //ship = new SpaceShip(GameControllerScript.instance.getRandomShipName(), 5);
        enemyShip = new SpaceShip(GameControllerScript.instance.getRandomShipName(), 5);
        GameControllerScript.instance.enemyShip = this.enemyShip;
        subject = enemyShip.SubjectReference();
        ship = GameControllerScript.instance.party.ship;
        getOptions();
        setSummary();

        EventPanelScript.instance.SetEvent(this);
    }

    public override void HandleEvent(OptionType oType)
    {
        switch (oType)
        {
            case (OptionType.Avoid):
                if (characters[0].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry("It's about to get spicy as " + characters[0].Name + " churns up the engines to the MAX. " + ship.Name + "'s thrusters fuckin' RIP OUT OF THERE.");
                    GameControllerScript.instance.LightYearsToEOU -= 3;
                    EventActions.loseResource(this, "Fuel");
                    GameControllerScript.instance.enemyShip = null;
                }
                else
                {
                    LogEntry("Maybe " + ship.Name + " isn't as nimble as we thought, maybe " + characters[0].Name + " is just a shit pilot, only time will tell...");
                    GameControllerScript.instance.party.ship.Damage(5);
                    EventActions.loseResource(this, "Fuel");
                }
                break;
            case (OptionType.Blast):
                if (characters[1].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry(ship.Name + " fires at full power, blasting " + subject + " to bits.");
                    GameControllerScript.instance.enemyShip = null;
                    EventActions.gainRandomResource(this);
                    GameControllerScript.instance.LightYearsToEOU -= 1;

                }
                else
                {
                    LogEntry(characters[1].Name + " misjudges the shot an leaves " + ship.Name + " vulnerable to counter fire.");
                    GameControllerScript.instance.party.ship.Damage(enemyShip.getStat("Blast"));
                }
                break;
            case (OptionType.Board):
                if (characters[2].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry(characters[2].Name + " boarded the enemy ship with ease! After a fierce battle on board you manage to steal some goods and make your way out.");
                    GameControllerScript.instance.enemyShip = null;
                    EventActions.gainRandomResource(this);
                    EventActions.gainRandomResource(this);
                }
                else
                {
                    LogEntry("The boarding was botched! You'll have to come around to try again.");
                    EventActions.loseResource(this, "Fuel");
                    GameControllerScript.instance.LightYearsToEOU -= 1;
                }
                break;
            case (OptionType.Negotiate):
                if (characters[3].getStat("Mind") + Random.Range(0, 3) > 6)
                {
                    LogEntry(characters[3].Name + " convices the enemies to stand down.");
                    GameControllerScript.instance.LightYearsToEOU -= 1;

                }
                else
                {
                    LogEntry("The enemies decide not to attack however you must send them some resources as tribute.");
                    EventActions.loseRandomResource(this);
                    GameControllerScript.instance.LightYearsToEOU -= 1;
                }
                GameControllerScript.instance.enemyShip = null;
                break;
        }
    }

    public override void setSummary()
    {
        summary = "You have encountered an enemy ship! On the side is it's name - " + subject;
    }

}


public class ShipCombatEvent : Event
{

    public ShipCombatEvent(EventType type, int id) : base(type, id)
    {

    }

    SpaceShip enemyShip;
    public override void EnterEvent()
    {

        characters.Clear();
        //This should be the enemy ship
        enemyShip = GameControllerScript.instance.enemyShip;
        subject = enemyShip.SubjectReference();
        ship = GameControllerScript.instance.party.ship;
        getOptions();
        setSummary();

        EventPanelScript.instance.SetEvent(this);
    }

    public override void HandleEvent(OptionType oType)
    {
        switch (oType)
        {
            case (OptionType.Flee):
                if (characters[0].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry("It's about to get spicy as " + characters[0].Name + " churns up the engines to the MAX. " + ship.Name + "'s thrusters fuckin' RIP OUT OF THERE.");
                    GameControllerScript.instance.LightYearsToEOU -= 3;
                    EventActions.loseResource(this, "Fuel");
                    GameControllerScript.instance.enemyShip = null;
                }
                else
                {
                    LogEntry("Maybe " + ship.Name + " isn't as nimble as we thought, maybe " + characters[0].Name + " is just a shit pilot, only time will tell...");
                    GameControllerScript.instance.party.ship.Damage(5);
                    GameControllerScript.instance.LightYearsToEOU -= 1;
                    EventActions.loseResource(this, "Fuel");
                }
                break;
            case (OptionType.Blast):
                if (characters[1].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry(ship.Name + " fires at full power, blasting " + subject + " to bits.");
                    GameControllerScript.instance.enemyShip = null;
                    EventActions.gainRandomResource(this);
                    GameControllerScript.instance.LightYearsToEOU -= 1;

                }
                else
                {
                    LogEntry(characters[1].Name + " misjudges the shot an leaves " + ship.Name + " vulnerable to counter fire.");
                    GameControllerScript.instance.party.ship.Damage(Random.Range(1, 10));

                }
                break;
            case (OptionType.Board):
                if (characters[2].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry(characters[2].Name + " boarded the enemy ship with ease! After a fierce battle on board you manage to steal some goods and make your way out.");
                    GameControllerScript.instance.enemyShip = null;
                    EventActions.gainRandomResource(this);
                    EventActions.gainRandomResource(this);
                }
                else
                {
                    LogEntry("The boarding was botched! You'll have to come around to try again.");
                    EventActions.loseResource(this, "Fuel");
                    GameControllerScript.instance.LightYearsToEOU -= 1;
                }
                break;
            case (OptionType.Negotiate):
                if (characters[3].getStat("Mind") + Random.Range(0, 3) > 6)
                {
                    LogEntry(characters[3].Name + " convices the enemies to stand down.");
                    GameControllerScript.instance.LightYearsToEOU -= 1;

                }
                else
                {
                    LogEntry("The enemies decide not to attack however you must send them some resources as tribute.");
                    EventActions.loseRandomResource(this);
                    GameControllerScript.instance.LightYearsToEOU -= 1;
                }
                GameControllerScript.instance.enemyShip = null;
                break;
        }
    }

    public override void setSummary()
    {
        summary = ship.Name + " is engaged in epic space combat with " + subject;
    }

}


public class EncounterNPC : Event
{

    public EncounterNPC(EventType type, int id) : base(type, id)
    {

    }

    public override void EnterEvent()
    {
        characters.Clear();

        characters.Add(GameControllerScript.instance.createNPC());
        subject = characters[0].SubjectReference();
        getOptions();
        setSummary();

        EventPanelScript.instance.SetEvent(this);
    }

    public override void HandleEvent(OptionType oType)
    {
        switch (oType)
        {
            case (OptionType.Recruit):
                if (characters[1].getStat("Mind") + Random.Range(0, 10) > 6)
                {
                    LogEntry(characters[1].Name + " convices " + characters[0].Name + " to join the party with a triumphant speech.");
                    GameControllerScript.instance.party.addPartyMember(characters[0]);
                    LogEntry(characters[0].Name + " joins the party!");
                    
                }
                else
                {
                    LogEntry("AWKWARD! " + characters[0].Name + " rejects the invitation from " + characters[1].Name + " to join the party.");
                }
                break;
            case (OptionType.Gossip):

                LogEntry(characters[2].Name + " learns about a nearby planet rich with resources from " + characters[0].Name);
                EventActions.gainRandomResource(this);
                GameControllerScript.instance.LightYearsToEOU -= 2;
                break;
            case (OptionType.Intimidate):
                if (characters[3].getStat("Strength") > Random.Range(0, 10))
                {
                    LogEntry(characters[0].Name + " backs away as " + characters[3].Name + " threatens them into giving up supplies.");
                    EventActions.gainRandomResource(this);
                }
                else
                {
                    LogEntry(characters[0].Name + " stands up to " + characters[3].Name + ", causing them to give up supplies in embarassment.");
                    EventActions.loseRandomResource(this);
                }
                break;
        }
        LogEntry("The crew fires up the ship and heads back into space.");
        GameControllerScript.instance.locationState = LocationState.Space;
    }

    public override void setSummary()
    {
        summary = "The party happens upon a mysterious stranger named " + characters[0].Name + ".";
    }
}

public class EncounterPlanet : Event
{

    public EncounterPlanet(EventType type, int id) : base(type, id)
    {

    }

    public override void EnterEvent()
    {
        characters.Clear();

        //characters.Add();
        subject = "Planet X01";
        getOptions();
        setSummary();

        EventPanelScript.instance.SetEvent(this);
    }

    public override void HandleEvent(OptionType oType)
    {
        switch (oType)
        {
            case (OptionType.Land):
                if (characters[0].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry(characters[0].Name + " has successfully landed on " + subject + " allowing the party to scavange for resources.");
                    EventActions.gainRandomResource(this);
                    EventActions.gainRandomResource(this);
                    GameControllerScript.instance.locationState = LocationState.Land;
                }
                else
                {
                    LogEntry("As " + characters[0].Name + " descends into the atmosphere, an electrical storm knocks out the " + GameControllerScript.instance.party.ship.Name + "'s engines, causing it to crash.");
                    GameControllerScript.instance.party.ship.Damage(5);

                    EventActions.loseResource(this, "Fuel");
                    GameControllerScript.instance.locationState = LocationState.Land;
                }
                break;
            case (OptionType.Scan):
                if (Random.Range(0, 100) > 60)
                {
                    LogEntry(characters[1].Name + " scans " + subject + " and determines it is rich with minerals.");
                    EventActions.gainRandomResource(this);
                } else
                {
                    LogEntry("The atmosphere of " + subject + " is creating too much interference to properly scan.");
                }
                EventActions.loseResource(this, "Fuel");
                break;
            case (OptionType.Bypass):

                LogEntry(characters[2].Name + " guides the ship around the planet.");
                    EventActions.loseResource(this, "Fuel");
                GameControllerScript.instance.LightYearsToEOU -= 2;
                break;
               
        }
    }

    public override void setSummary()
    {
        summary = "The party spots a planet nearby. A quick XooXle search reveals it's name - " + subject;
    }
}

