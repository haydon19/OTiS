using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitType { Engineer };

public class Trait {

    string name;
    string description;
    List<StatBonus> statBonuses;
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

    public List<StatBonus> StatBonuses
    {
        get
        {
            return statBonuses;
        }

        set
        {
            statBonuses = value;
        }
    }

    public Trait(string name, string description, List<StatBonus> bonuses)
    {
        this.name = name;
        this.description = description;
        StatBonuses = bonuses;
    }
}
