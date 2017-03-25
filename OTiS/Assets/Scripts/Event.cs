using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { Greeting, Combat, Decision, Outcome, Death, NewEnemy, NewAlly, GameOver, Charm, Run_Pass, Run_Fail, EnemyShip, ShipDestroyed, SpaceCombat, RogueAstroid };

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
        EventLog.instance.newLogItem(summary, type);
    }

   
    public virtual void setSummary()
    {
        summary = "Default Summary";
    }

}



public class StatementEvent : Event
{

    public StatementEvent(EventType type, int id) : base(type, id)
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
        setSummary();
        LogEvent();
    }

    public override void setSummary()
    {
        summary = " The party has died. Game Over.";

    }

}

public class NewEnemyEvent : Event
{
    Character enemy;

    public NewEnemyEvent(EventType type, int ID) : base(type, ID)
    {
        GameControllerScript gc = GameControllerScript.instance;
        enemy = gc.getRandomEnemy();
        GameControllerScript.instance.Enemies.Add(enemy);
        EnemyInfoPanel.instance.newCharacter(enemy);

        summary = enemy.Name + " has appeared and looks hostile! What do you do?";

        Options = new List<Option>();
        Options.Add(new Option(this, OptionType.Fight, enemy, gc.getRandomPartyMember()));
        Options.Add(new Option(this, OptionType.Charm, enemy, gc.getRandomPartyMember()));
        Options.Add(new Option(this, OptionType.Run, enemy, gc.getRandomPartyMember()));
        OptionMenuController.instance.addOptionItems(this);
        GameControllerScript.instance.choosing = true;
        LogEvent();

    }

    public override void setSummary()
    {
        summary = enemy.Name + " has appeared and looks hostile! What do you do?";

    }

}

public class CharacterCombatEvent : Event
{
    Character player, enemy;

    public CharacterCombatEvent(EventType type, int id, Character player, Character enemy) : base(type, id)
    {
        this.player = player;
        this.enemy = enemy;
        setSummary();
        enemy.Dead = true;
        EnemyInfoPanel.instance.removeCharacter(enemy);
        GameControllerScript.instance.Enemies.Remove(enemy);
        LogEvent();

    }
    public override void setSummary()
    {
        if (player.getStat("Strength") >= enemy.getStat("Strength"))
        {
            summary = player.Name + " has fought off the " + enemy.Name + " without harm.";
        }
        else
        {
            summary = player.Name + " is overwhelmed by " + enemy.Name + "'s strength and suffers " + enemy.Attack(player) + " damage fighting it off.";
            if (player.Dead)
            {
                CharacterInfoPanel.instance.removeCharacter(player);
                GameControllerScript.instance.Party.Remove(player);
            }

        }
    }

}

public class RunEvent : Event
{
    Character player, enemy;
    public RunEvent(EventType type, int id, Character player, Character enemy) : base(type, id)
    {
        this.player = player;
        this.enemy = enemy;
        setSummary();
        enemy.Dead = true;
        EnemyInfoPanel.instance.removeCharacter(enemy);
        GameControllerScript.instance.Enemies.Remove(enemy);
        LogEvent();

    }

    public override void setSummary()
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
    }

}



public class CharmEvent : Event
{
    Character player, enemy;

    public CharmEvent(EventType type, int id, Character player, Character enemy) : base(type, id)
    {
        this.player = player;
        this.enemy = enemy;
        setSummary();
        LogEvent();
    }

    public override void setSummary()
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
    }

}

public class RogueAstroidEvent : Event
{

    public RogueAstroidEvent(EventType type, int id) : base(type, id)
    {
        GameControllerScript gc = GameControllerScript.instance;

        getCharacters(3);

        setSummary();
        Options = new List<Option>();
        Options.Add(new Option(this, OptionType.Avoid, characters[0]));
        Options.Add(new Option(this, OptionType.Blast, characters[1]));
        Options.Add(new Option(this, OptionType.Land, characters[2]));
        OptionMenuController.instance.addOptionItems(this);
        GameControllerScript.instance.choosing = true;
        LogEvent();
    }

    public void HandleEvent(OptionType oType)
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
        summary = "You are travelling through space...";
    }

}