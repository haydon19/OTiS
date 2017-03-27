using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { Statement, Death, GameOver, EnemyShip, SpaceCombat, RogueAstroid, EncounterNPC};

public abstract class Event {

    
    protected string summary;
    int id;
    List<Option> options;
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


    public void LogEvent()
    {
        EventLog.instance.newLogItem(summary);
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

        LogEvent();

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
            Options.Add(new Option(this, o, c));
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
        LogEvent();
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
        LogEvent();
    }

    public override void setSummary()
    {
        summary = " The party has died. Game Over.";

    }

}

public class RogueAstroidEvent : Event
{

    public RogueAstroidEvent(EventType type, int id) : base(type, id)
    {

    }

    public override void EnterEvent()
    {
        characters.Clear();

        getOptions();
        setSummary();

        LogEvent();
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
                        summary = "You seem to be at one with the controls of the ship, you feel your skills as a pilot grow!\n\n" + characters[0].Name + "'s Piloting increases! " + characters[0].getStat("Piloting");
                    }
                    else {
                        summary = "You race to the helm, and dive into the pilots seat in an effort to steer the ship clear of the asteroid. A narrow success.";
                    }
                }
                else {

                    if (randomSkillLoss(characters[0], "Piloting"))
                    {
                        characters[0].changeStat("Piloting", -1);
                        summary = "You scrape against the asteriods hard exterior, a compartment of the ship holding ESSENTIAL supplies has been damaged and it's contents blown into space! Maybe you weren't meant to hang around the pilots seat...\n\n" + characters[0].Name + "'s Piloting decreases!  " + characters[0].getStat("Piloting");
                    }
                    else {
                        summary = "Your heroic dive to the pilots seat is misaligned, luckily it wasn't the hardest asteroid in the galaxy, and your ship destroys it with it's hull.";
                    }
                }
            break;
            case (OptionType.Blast):
                if (characters[0].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    summary = "Ya fuckin blasted it bud.";
                }
                else
                {
                    summary = "Ya fucked up blasting it.";
                }
                break;
            case (OptionType.Land):
                if (characters[0].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    summary = "Ya fuckin landed on it bud.";
                }
                else
                {
                    summary = "Ya fucked up the landing.";
                }
                break;
        }
        LogEvent();
    }

    public override void setSummary()
    {
        summary = "You are travelling through space... A Rogue Astroid appears!";
    }

}

public class EnemyShipEvent : Event
{

    public EnemyShipEvent(EventType type, int id) : base(type, id)
    {

    }

    public override void EnterEvent()
    {

        characters.Clear();

        getOptions();
        setSummary();

        LogEvent();
    }

    public override void HandleEvent(OptionType oType)
    {
        switch (oType)
        {
            case (OptionType.Avoid):
                if (characters[0].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    summary = "It's about to get spicy as " + characters[0].Name + " churns up the engines to the MAX. " + ship.Name + "'s thrusters fuckin' RIP OUT OF THERE.";
                }
                else
                {
                    summary = "Maybe " + ship.Name + " isn't as nimble as we thought, maybe " + characters[0].Name + " is just a shit pilot, only time will tell...";
                }
                break;
            case (OptionType.Blast):
                if (characters[1].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    summary = "Ya fuckin blasted it bud.";
                }
                else
                {
                    summary = "Ya fucked up blasting it.";
                }
                break;
            case (OptionType.Board):
                if (characters[2].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    summary = characters[2].Name + " boarded the enemy ship with ease!";
                }
                else
                {
                    summary = "Ya fucked up the boarding.";
                }
                break;
        }
        LogEvent();
    }

    public override void setSummary()
    {
        summary = "You have encountered an enemy ship!";
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
        getOptions();
        setSummary();

        LogEvent();
    }

    public override void HandleEvent(OptionType oType)
    {
        switch (oType)
        {
            case (OptionType.Recruit):
                if (characters[1].getStat("Smarts") + Random.Range(0, 3) > 6)
                {
                    summary = characters[1].Name + " convices " + characters[0].Name + " to join the party with a triumphant speech.";
                    GameControllerScript.instance.party.addPartyMember(characters[0]);
                    summary = "\n" + characters[0].Name + " joins the party!";
                    
                }
                else
                {
                    summary = "AWKWARD! " + characters[0].Name + " rejects the invitation from " + characters[1].Name + " to join the party.";
                }
                break;
            case (OptionType.Gossip):

                summary = characters[2].Name + " learns about a nearby planet rich with resources from " + characters[0].Name;

                break;
            case (OptionType.Intimidate):
                if (characters[3].getStat("Strength") > Random.Range(0, 10))
                {
                    summary = characters[0].Name + " backs away as " + characters[3].Name + " threatens them into giving up supplies.";
                }
                else
                {
                    summary = characters[0].Name + " stands up to " + characters[3].Name + ", causing them to give up supplies in embarassment.";
                }
                break;
        }
        LogEvent();
    }

    public override void setSummary()
    {
        summary = "The party happens upon a mysterious stranger named " + characters[0].Name + ".";
    }
}