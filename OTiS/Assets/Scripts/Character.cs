using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : IAttacker<IDamageable<float>>, IDamageable<float> {
    Dictionary<string, int> stats = new Dictionary<string, int>();
    string name;
    string charID;
    int iD;
    bool dead;

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

    public Character(string name, int ID, int Strength, int Smarts, int Agility)
    {
        Dead = false;
        this.name = name;
        this.ID = ID;
        this.CharID = name + ID;
        setStat("Strength", Strength);
        setStat("Smarts", Smarts);
        setStat("Agility", Agility);
        setStat("Piloting", 5);
        setStat("Level", 1);
        setStat("Health", 15);
    }

    public float Attack(IDamageable<float> target)
    {
        return target.Damage(getStat("Strength"));
    }

    public virtual float Damage(float damageTaken)
    {
        setStat("Health", getStat("Health") - (int)damageTaken);
        CharacterInfoPanel.instance.updateCharacterInfo(this);
        if(getStat("Health") <= 0)
        {
            Dead = true;
        }
        return damageTaken;
    }

    public void Charm()
    {
        Debug.Log(Name + " has been charmed.");
        GameControllerScript.instance.Enemies.Remove(this);
        EnemyInfoPanel.instance.removeCharacter(this);
        Character temp = new Character(Name, ID, getStat("Strength"), getStat("Smarts"), getStat("Agility"));
        GameControllerScript.instance.Party.Add(temp);
        CharacterInfoPanel.instance.newCharacter(temp);

    }

    //Dealing with the player stat dictionary
    public int getStat(string statName)
    {
        if (!stats.ContainsKey(statName))
        {
            stats.Add(statName, 0);
        }

        //returns the value of the given stat
        return stats[statName];

    }

    public void setStat(string statName, int statValue)
    {
        if (stats.ContainsKey(statName))
        {
            stats[statName] = statValue;

        }
        else
        {
            //If the stat doesnt exist and we are setting it, lets just create it
            stats.Add(statName, statValue);
        }
    }

    public void changeStat(string statName, int statChange)
    {
        if (stats.ContainsKey(statName))
        {
            stats[statName] = getStat(statName) + statChange;
            if (stats[statName] < 0)
            {
                stats[statName] = 0;
            }

        }
        else
        {
            //If the stat doesnt exist and we are setting it, lets just create it
            stats.Add(statName, statChange);
        }
    }

}
