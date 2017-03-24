using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { Greeting, Combat, Decision, Outcome, Death, NewEnemy, NewAlly, GameOver, Charm, Run_Pass, Run_Fail, EnemyShip, ShipDestroyed, SpaceCombat };

public abstract class Event {

    
    protected string summary;
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

    /*
    public Event(EventType type, List<Character> participants, int id) {
        this.id = id;
        
        options = new List<Option>();
        this.type = type;
        //If i cant think of a better way, I'm going to make these into subclasses
        //probably something like Nonconsequential, Combat, Problem
        //Then if a noncombat event ended in combat, we could just create a combat event and chain from there
        switch (type)
        {

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
                        } else if (c is Character)
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
            case EventType.EnemyShip:
                summary = "An enemy spaceship has appeared!";
                break;
            case EventType.SpaceCombat:
                SpaceShip enemy, player;
                enemy = GameControllerScript.instance.enemyShip;
                player = GameControllerScript.instance.ship;
                summary = enemy.Name + " deals " + enemy.Attack(player) + " damage to " + player.Name;

                summary += "\n" + player.Name + " deals " + player.Attack(enemy) + " damage to " + enemy.Name;
                break;
        }

        EventLog.instance.newLogItem(this);


    }
    */

    public Event(EventType type, int ID)
    {

        id = ID;
        this.type = type;
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

    public void LogEvent()
    {
        EventLog.instance.newLogItem(this);
    }

   

}



public class StatementEvent : Event
{

    public StatementEvent(EventType type, int id, Character actor) : base(type, id)
    {
        summary = actor.Name + " makes a statement.";
        LogEvent();

    }

}

public class GameOverEvent : Event
{

    public GameOverEvent(EventType type, int id) : base(type, id)
    {
        summary = " The party has died. Game Over.";
        LogEvent();
    }

}

public class NewEnemyEvent : Event
{
    public NewEnemyEvent(EventType type, int ID) : base(type, ID)
    {
        GameControllerScript gc = GameControllerScript.instance;
        Enemy enemy = gc.getRandomEnemy();
            GameControllerScript.instance.Enemies.Add(enemy);
        EnemyInfoPanel.instance.newCharacter(enemy);

        summary = enemy.Name + " has appeared and looks hostile! What do you do?";

        Options = new List<Option>();
        Options.Add(new Option(OptionType.Fight, enemy, gc.getRandomPartyMember()));
        Options.Add(new Option(OptionType.Charm, enemy, gc.getRandomPartyMember()));
        Options.Add(new Option(OptionType.Run, enemy, gc.getRandomPartyMember()));
        OptionMenuController.instance.addOptionItems(this);
        GameControllerScript.instance.choosing = true;
        LogEvent();

    }
}

public class CharacterCombatEvent : Event
{

    public CharacterCombatEvent(EventType type, int id, Character player, Character enemy) : base(type, id)
    {
       
        if (player.getStat("Strength") >= enemy.getStat("Strength"))
        {
            summary = player.Name + " has fought off the " + enemy.Name + " without much harm.";
        }
        else
        {
            summary = player.Name + " is overwhelmed by " + enemy.Name + "'s strength and suffers " + enemy.Attack(player) + " damage while fighting it off.";
            if (player.Dead)
            {
                CharacterInfoPanel.instance.removeCharacter(player);
                GameControllerScript.instance.Party.Remove(player);
            }
            
        }
        enemy.Dead = true;
        EnemyInfoPanel.instance.removeCharacter(enemy);
        GameControllerScript.instance.Enemies.Remove(enemy);
        LogEvent();

    }

}

public class RunEvent : Event
{

    public RunEvent(EventType type, int id, Character player, Character enemy) : base(type, id)
    {
       
        if (player.getStat("Agility") >= enemy.getStat("Agility"))
        {
            summary = player.Name + " has lead the party away from the " + enemy.Name + " without trouble.";
        }
        else
        {
            summary = player.Name + " cannot escape " + enemy.Name + " and suffers " + enemy.Attack(player) + " damage while trying to run.";
            if (player.Dead)
            {
                CharacterInfoPanel.instance.removeCharacter(player);
                GameControllerScript.instance.Party.Remove(player);
            }
            
        }
        enemy.Dead = true;
        EnemyInfoPanel.instance.removeCharacter(enemy);
        GameControllerScript.instance.Enemies.Remove(enemy);
        LogEvent();

    }

}

public class CharmEvent : Event
{

    public CharmEvent(EventType type, int id, Character player, Character enemy) : base(type, id)
    {
        
        if (player.getStat("Smarts") >= enemy.getStat("Smarts"))
        {
            summary = player.Name + " convices the " + enemy.Name + " to join the party.";
            enemy.Charm();
        }
        else
        {
            summary = player.Name + " fumbles their words, enraging " + enemy.Name + " and suffers " + enemy.Attack(player) + " damage as a result.";
            if (player.Dead)
            {
                CharacterInfoPanel.instance.removeCharacter(player);
                GameControllerScript.instance.Party.Remove(player);
            }
            enemy.Dead = true;
            EnemyInfoPanel.instance.removeCharacter(enemy);
            GameControllerScript.instance.Enemies.Remove(enemy);
            

        }
        LogEvent();
    }

}