using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public enum OptionType {  };


    public enum OptionType { Fight, Run, Charm };
public class Option {
    //string type;
    string name;
    string description;
    string outcome;
    string stat;
    float successRate;
    List<Character> actors;
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

    public Option(OptionType type, List<Character> participants)
    {


        this.type = type;
        actors = new List<Character>(participants);

        switch (type)
        {

            case (OptionType.Fight):
                description = actors[1].Name + " fights " + actors[0].Name + " (Strength)";
                break;
            case (OptionType.Charm):
                description = actors[1].Name + " attempts to charm " + actors[0].Name + " (Smarts)";
                break;
            case (OptionType.Run):
                description = "The party runs away from " + actors[0].Name + " (Agility)";
                break;
        }
    }

    public void OptionChosen()
    {
        Event outcomeEvent;
        //if the outcome succeeded
        //int r = Random.Range(1, 13);
        //Debug.Log(actors[0].Name + " rolled " + r + " and needed less than " + actors[0].getStat(stat));


        switch (type)
        {

            case (OptionType.Fight):
                outcomeEvent = new Event(EventType.Combat, actors , EventLog.instance.transform.childCount);
                //outcomeEvent.Summary = actor.Name + " succeeded in " + description + ". " + actor.Name + " gained 1 point in " + stat;

                GameControllerScript.instance.choosing = false;
                
                break;
            case (OptionType.Charm):
                outcomeEvent = new Event(EventType.Charm, actors, EventLog.instance.transform.childCount);
                //outcomeEvent.Summary = actor.Name + " succeeded in " + description + ". " + actor.Name + " gained 1 point in " + stat;

                GameControllerScript.instance.choosing = false;
                break;
            case (OptionType.Run):
                outcomeEvent = new Event(EventType.Combat, actors, EventLog.instance.transform.childCount);
                //outcomeEvent.Summary = actor.Name + " succeeded in " + description + ". " + actor.Name + " gained 1 point in " + stat;

                GameControllerScript.instance.choosing = false;
                break;

        }

        OptionMenuController.instance.clearOptions();

    }

}
