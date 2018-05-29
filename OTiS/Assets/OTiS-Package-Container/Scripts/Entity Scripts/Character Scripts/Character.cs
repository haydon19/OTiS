using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : IAttacker<IDamageable<int>>, IDamageable<int>, ISubject {
    Dictionary<string, int> stats = new Dictionary<string, int>();
    string name;
    string charID;
    string race;
    string gender;
    int iD;
    bool dead;
    public List<Trait> traits;
    Stats charStats;
    private int strength;
    private int Mind;
    private int agility;
    private int piloting;

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

    public string CharID
    {
        get
        {
            return charID;
        }

        set
        {
            charID = value;
        }
    }

    public bool Dead
    {
        get
        {
            return dead;
        }

        set
        {
            dead = value;
        }
    }

    public int ID
    {
        get
        {
            return iD;
        }

        set
        {
            iD = value;
        }
    }

    public Dictionary<string, int> Stats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    public string Race
    {
        get
        {
            return race;
        }

        set
        {
            race = value;
        }
    }

    public string Gender
    {
        get
        {
            return gender;
        }

        set
        {
            gender = value;
        }
    }

    public Stats CharStats
    {
        get
        {
            return charStats;
        }

        set
        {
            charStats = value;
        }
    }

    public Character(string name, string race, string gender, int ID, int Strength, int Mind, int Agility, int Piloting)
    {
        Dead = false;
        this.name = name;
        this.race = race;
        this.gender = gender;
        this.ID = ID;
        this.CharID = name + ID;

        charStats = new Stats();
        setStat("Strength", Strength);
        setStat("Mind", Mind);
        setStat("Agility", Agility);
        setStat("Piloting", Piloting);
        setStat("Level", 1);
        setStat("Health", 15);

        traits = new List<Trait>();
    }

    public Character(string name, int iD, int strength, int Mind, int agility, int piloting)
    {
        this.name = name;
        this.iD = iD;
        this.strength = strength;
        this.Mind = Mind;
        this.agility = agility;
        this.piloting = piloting;
    }

    public int Attack(IDamageable<int> target)
    {
        return target.Damage(getStat("Strength"));
    }

    public virtual int Damage(int damageTaken)
    {
        //setStat("Health", getStat("Health") - (int)damageTaken);
        changeStat("Health", -damageTaken);
        //CharacterContainer.instance.updateCharacterInfo(this);
        if (getStat("Health") <= 0)
        {
            Dead = true;
        }
        return damageTaken;
    }

    /*
    public void Charm()
    {
        Debug.Log(Name + " has been charmed.");
        GameControllerScript.instance.Enemies.Remove(this);
        EnemyInfoPanel.instance.removeCharacter(this);
        //Character temp = new Character(Name, ID, getStat("Strength"), getStat("Mind"), getStat("Agility"), getStat("Piloting"));
        GameControllerScript.instance.party.addPartyMember(temp);
        //CharacterContainer.instance.addCharacter(temp);

    }

    */

    public bool hasTrait(string traitName)
    {
        bool check = false;
        foreach(Trait t in traits)
        {
            if (t.Name == traitName)
            {
                check = true;
                break;
            }
        }

        return check;
    }

    public void addTrait(Trait trait)
    {
        traits.Add(trait);
    }

    //Dealing with the player stat dictionary
    public int getStat(string statName)
    {
        if (!Stats.ContainsKey(statName))
        {
            Stats.Add(statName, 0);
        }

        //returns the value of the given stat
        return Stats[statName];

    }

    public void setStat(string statName, int statValue)
    {
        if (Stats.ContainsKey(statName))
        {
            Stats[statName] = statValue;

        }
        else
        {
            //If the stat doesnt exist and we are setting it, lets just create it
            Stats.Add(statName, statValue);
        }
    }

    public void changeStat(string statName, int statChange)
    {
        if (Stats.ContainsKey(statName))
        {
            Stats[statName] = getStat(statName) + statChange;
            if (Stats[statName] < 0)
            {
                Stats[statName] = 0;
            }

        }
        else
        {
            //If the stat doesnt exist and we are setting it, lets just create it
            Stats.Add(statName, statChange);
        }
    }

    public string SubjectReference()
    {
        return name;
    }
}
