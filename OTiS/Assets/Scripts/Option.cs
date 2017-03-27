using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public enum OptionType {  };


    public enum OptionType { Avoid, Blast, Land, Board, Thrusters, Comms,Intimidate, Recruit, Gossip };
public class Option {
    //string type;
    string name;
    string description;
    string outcome;
    string stat;
    string subject;
    Character actor;
    //List<Character> actors;
    Event eventObject;
    OptionType type;

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


    public Option(Event eventObject, OptionType type, Character actor, string subject)
    {
        this.eventObject = eventObject;
        this.subject = subject;
        this.type = type;
        this.actor = actor;

        setDescription();
    }

    public void setDescription()
    {
        string s = GameControllerScript.instance.optionStrings[type][Random.Range(0, GameControllerScript.instance.optionStrings[type].Count)];
        description = s.Replace("<actor>", actor.Name).Replace("<subject>", subject);
    }


    public Option(EncounterNPC eventObject, OptionType type, Character actor)
    {
        this.eventObject = eventObject;

        this.type = type;
        this.actor = actor;

        description = actor.Name + GameControllerScript.instance.optionStrings[type][Random.Range(0, GameControllerScript.instance.optionStrings[type].Count)] + eventObject.characters[0].Name;

    }

    public void OptionChosen()
    {

        eventObject.HandleEvent(type);
        OptionMenuController.instance.clearOptions();
        GameControllerScript.instance.choosing = false;

    }

}
