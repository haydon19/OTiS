using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SituationTag { Statement, Death, GameOver, EncounterShip, SpaceCombat, EncounterAsteroid, EncounterNPC, EncounterPlanet, EmptyShip, ShipCombat };
public enum SubjectTag { Planet, Ship, CrewMember, Asteroid, NPC };

public abstract class Situation :ScriptableObject {

    [SerializeField]
    [TextArea(5, 10)]
    protected string summary;

    protected string logEntry;
    [SerializeField]
    int id;
    [SerializeField]
    List<Option> options;
    [SerializeField]
    public string subject;
    [SerializeField]
    SituationTag sitchTag;
    public List<Character> characters = new List<Character>();
    public SpaceShip ship;
    [SerializeField]
    public Sprite sprite;

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

    public SituationTag SitchTag
    {
        get
        {
            return sitchTag;
        }

        set
        {
            sitchTag = value;
        }
    }

    public Situation(SituationTag type, int ID)
    {
        characters = new List<Character>();
        id = ID;
        this.sitchTag = type;

    }

    public void GetCharacters(int num)
    {
        for (int i = 0; i < num; i++)
        {
            characters.Add(GameControllerScript.instance.getRandomPartyMember());
        }
    }


    public void LogSummary()
    {
        EventLog.instance.newLogItem(summary);
    }

    public void LogEntry(string entry)
    {
        EventLog.instance.newLogItem(entry);
    }



    public virtual void SetSummary()
    {
        summary = "Default Summary";
    }

    public virtual void Initialize()
    {

        characters = new List<Character>();

        SetOptions();
        SetSummary();

        LogSummary();

    }

    public virtual void HandleEvent(OptionTag oType)
    {
        OptionMenuController.instance.clearOptions();
        GameControllerScript.instance.choosing = false;
    }

    public virtual void SetOptions()
    {

        Character c;
        foreach (Option o in options)
        {
            c = GameControllerScript.instance.getRandomPartyMember();
            characters.Add(c);
            //Options.Add(new Option(this, o, c, subject));
        }

        OptionMenuController.instance.AddOptionObjects(this);
        GameControllerScript.instance.choosing = true;
    }
}










