using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBonus
{
    string statName;
    int bonusValue;

    public string StatName
    {
        get
        {
            return statName;
        }

        set
        {
            statName = value;
        }
    }

    public int BonusValue
    {
        get
        {
            return bonusValue;
        }

        set
        {
            bonusValue = value;
        }
    }

    public StatBonus(string stat, int value)
    {
        statName = stat;
        bonusValue = value;

    }

}