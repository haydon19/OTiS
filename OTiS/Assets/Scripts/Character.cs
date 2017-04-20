﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : IAttacker<IDamageable<float>>, IDamageable<float>, ISubject {
    Dictionary<string, int> stats = new Dictionary<string, int>();
    string name;
    string charID;
    string race;
    string gender;
    int iD;
    bool dead;
    public List<CharacterTrait> traits;
    private int strength;
    private int smarts;
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

    public Character(string name, string race, string gender, int ID, int Strength, int Smarts, int Agility, int Piloting)
    {
        Dead = false;
        this.name = name;
        this.race = race;
        this.gender = gender;
        this.ID = ID;
        this.CharID = name + ID;
        setStat("Strength", Strength);
        setStat("Smarts", Smarts);
        setStat("Agility", Agility);
        setStat("Piloting", Piloting);
        setStat("Level", 1);
        setStat("Health", 15);

        traits = new List<CharacterTrait>();
    }

    public Character(string name, int iD, int strength, int smarts, int agility, int piloting)
    {
        this.name = name;
        this.iD = iD;
        this.strength = strength;
        this.smarts = smarts;
        this.agility = agility;
        this.piloting = piloting;
    }

    public float Attack(IDamageable<float> target)
    {
        return target.Damage(getStat("Strength"));
    }

    public virtual float Damage(float damageTaken)
    {
        setStat("Health", getStat("Health") - (int)damageTaken);
        CharacterContainer.instance.updateCharacterInfo(this);
        if(getStat("Health") <= 0)
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
        //Character temp = new Character(Name, ID, getStat("Strength"), getStat("Smarts"), getStat("Agility"), getStat("Piloting"));
        GameControllerScript.instance.party.addPartyMember(temp);
        //CharacterContainer.instance.addCharacter(temp);

    }

    */

    public bool hasTrait(string traitName)
    {
        bool check = false;
        foreach(CharacterTrait t in traits)
        {
            if (t.Name == traitName)
            {
                check = true;
                break;
            }
        }

        return check;
    }

    public void addTrait(CharacterTrait trait)
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
