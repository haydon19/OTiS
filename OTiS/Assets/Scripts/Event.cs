using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { Greeting, Combat, Decision, Outcome, Death, NewEnemy, NewAlly, GameOver, Charm, Run_Pass, Run_Fail };

public class Event {

    
    string summary;
    int id;
    List<Option> options;
    EventType type;
    

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

    public Event(EventType type, List<Character> participants, int id) {
        this.id = id;
        
        options = new List<Option>();
        this.type = type;
        //If i cant think of a better way, I'm going to make these into subclasses
        //probably something like Nonconsequential, Combat, Problem
        //Then if a noncombat event ended in combat, we could just create a combat event and chain from there
        switch (type)
        {
            case EventType.Greeting:
                summary = participants[0].Name + " says hello to " + participants[1].Name;
                break;

            case EventType.Combat:
                summary = participants[0].Name + " deals " + participants[0].Attack(participants[1]) + " damage to " + participants[1].Name;
                
                summary += "\n" + participants[1].Name + " deals " + participants[1].Attack(participants[0]) + " damage to " + participants[0].Name;

                foreach (Character c in participants)
                {
                    if (c.Dead)
                    {
                        summary += "\n" + c.Name + " has died.";

                        if (c is Enemy)
                        {
                            EnemyInfoPanel.instance.removeCharacter(c);
                            GameControllerScript.instance.Enemies.Remove(c);
                        } else
                        {
                            CharacterInfoPanel.instance.removeCharacter(c);
                            GameControllerScript.instance.Party.Remove(c);
                        }
                    }
                }
                break;
            case EventType.Run_Pass:
                summary = participants[1].Name + " safely leads the party away from " + participants[0].Name + ".";
                EnemyInfoPanel.instance.removeCharacter(participants[0]);
                GameControllerScript.instance.Enemies.Remove(participants[0]);
                break;
            case EventType.Run_Fail:
                summary = participants[1].Name + " cannot escape " + participants[0].Name + ".";
                break;
            case EventType.Death:
                summary = participants[0].Name + " has Died.";
                break;
            case EventType.Charm:
                summary = participants[0].Name + " has been charmed and joined the party.";
                participants[0].Charm();
                break;
            case EventType.NewEnemy:
                summary = participants[0].Name + " has appeared and looks hostile! What do you do?";

                Options.Add(new Option(OptionType.Fight, participants));
                Options.Add(new Option(OptionType.Charm, participants));
                Options.Add(new Option(OptionType.Run, participants));
                OptionMenuController.instance.addOptionItems(this);
                GameControllerScript.instance.choosing = true;

                break;
            case EventType.GameOver:
                summary = "The Party has died. Game Over.";
                break;       
        }

        EventLog.instance.newLogItem(this);


    }

    /*
    public void addOptions()
    {
        foreach (Option o in options)
        {
            OptionMenuController.instance.addOptionItem(o);
        }
    }
    */

    public void setSummary()
    {
        

    }

   

}

/*
public class CombatEvent : Event
{

    public CombatEvent(EventType type, int id, List<Character> participants) : base(type, participants, id)
    {
        Summary = participants[0].Name + " deals " + participants[0].Attack(participants[1]) + " damage to " + participants[1].Name;
    }

}
*/
