using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StatModType { Add, Mult };

public class Stats {
    Dictionary<string, int> baseStats;
    Dictionary<string, StatModifier> statModifiers;

    public Stats()
    {
        baseStats = new Dictionary<string, int>();

    }

    public int getBaseStatValue(string stat)
    {
        return baseStats[stat];
    }

    public void addModifier(string id, StatModifier modifier)
    {
        statModifiers.Add(id, modifier);
    }

    public void removeModifier(string id)
    {
        statModifiers.Remove(id);
    }

    public int Get(string stat) {
        int total = baseStats[stat];
        float multiplier = 0;
        foreach(KeyValuePair<string, StatModifier> mod in statModifiers)
        {
            if(mod.Value.Stat == stat)
            {
                switch (mod.Value.Type)
                {
                    case StatModType.Add:
                        total += mod.Value.ModValue;

                        break;
                    case StatModType.Mult:
                        multiplier += mod.Value.ModValue;

                        break;
                }  
            }

        }

        return total + (int)(total * multiplier);

    }

}



public class StatModifier
{
    StatModType type;
    int modValue;
    string modId;
    string stat;

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

    public string ModId
    {
        get
        {
            return modId;
        }

        set
        {
            modId = value;
        }
    }

    public int ModValue
    {
        get
        {
            return modValue;
        }

        set
        {
            modValue = value;
        }
    }

    public StatModType Type
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

    public StatModifier(string id, StatModType t, int v, string s)
    {
        ModId = id;
        Type = t;
        ModValue = v;
        Stat = s;


    }

}
