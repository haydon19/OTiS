using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public enum OptionType {  };


    public enum OptionType { Avoid, Blast, Land, Board, Thrusters, Comms };
public class Option {
    //string type;
    string name;
    string description;
    string outcome;
    string stat;
    float successRate;
    Character subject;
    Character actor;
    //List<Character> actors;
    Event eventObject;
    OptionType type;

    public float SuccessRate
    {
        get
        {
            return successRate;
        }

        set
        {
            successRate = value;
        }
    }

    public string Stat
    {
        get
        {
            return stat;
        }

        set
        {
            stat = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

 

    public OptionType Type
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


    /*

    public Option(Event eventObject, OptionType type, Character subject, Character actor)
    {

        this.eventObject = eventObject;
        this.type = type;
        //actors = new List<Character>(participants);
        this.actor = actor;
        this.subject = subject;
        switch (type)
        {

            case (OptionType.Fight):
                description = actor.Name + " fights " + subject.Name + ". (Strength)";
                break;
            case (OptionType.Charm):
                description = actor.Name + " attempts to charm " + subject.Name + ". (Smarts)";
                break;
            case (OptionType.Run):
                description = actor.Name + " tries to run from " + subject.Name + ". (Agility)";
                break;
        }
    }
    */

    public Option(Event eventObject, OptionType type, Character actor)
    {
        this.eventObject = eventObject;

        this.type = type;
        this.actor = actor;

        description = actor.Name + GameControllerScript.instance.optionStrings[type][Random.Range(0, GameControllerScript.instance.optionStrings[type].Count)] + eventObject.ToString();

    }

    public void OptionChosen()
    {

        eventObject.HandleEvent(type);
        OptionMenuController.instance.clearOptions();
        GameControllerScript.instance.choosing = false;

        /*

        case (OptionType.Fight):
            outcomeEvent = new CharacterCombatEvent(EventType.Combat, EventLog.instance.transform.childCount, actor, subject);
            //outcomeEvent.Summary = actor.Name + " succeeded in " + description + ". " + actor.Name + " gained 1 point in " + stat;

            GameControllerScript.instance.choosing = false;

            break;
        case (OptionType.Charm):
            outcomeEvent = new CharmEvent(EventType.Charm, EventLog.instance.transform.childCount, actor, subject);
            //outcomeEvent.Summary = actor.Name + " succeeded in " + description + ". " + actor.Name + " gained 1 point in " + stat;

            GameControllerScript.instance.choosing = false;
            break;
        case (OptionType.Run):

                outcomeEvent = new RunEvent(EventType.Run_Fail, EventLog.instance.transform.childCount, actor, subject);

            //outcomeEvent.Summary = actor.Name + " succeeded in " + description + ". " + actor.Name + " gained 1 point in " + stat;

            GameControllerScript.instance.choosing = false;
            break;
            */






    }

}
