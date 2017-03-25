using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { Statement, Death, GameOver, EnemyShip, SpaceCombat, RogueAstroid };

public abstract class Event {

    
    protected string summary;
    int id;
    List<Option> options;
    EventType type;
    public List<Character> characters;



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

    }

    public virtual void HandleEvent(OptionType oType)
    {

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
        setSummary();
        characters.Clear();

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

        LogEvent();
    }

    public override void HandleEvent(OptionType oType)
    {
        switch (oType)
        {
            case (OptionType.Avoid):
                if(characters[0].getStat("Piloting") + Random.Range(0,3) > 6)
                {
                    summary = "Ya fuckin avoided it bud.";
                } else
                {
                    summary = "Ya fucked up avoiding.";
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
        setSummary();
        characters.Clear();

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

        LogEvent();
    }

    public override void HandleEvent(OptionType oType)
    {
        switch (oType)
        {
            case (OptionType.Avoid):
                if (characters[0].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    summary = "Ya fuckin avoided it bud.";
                }
                else
                {
                    summary = "Ya fucked up avoiding.";
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